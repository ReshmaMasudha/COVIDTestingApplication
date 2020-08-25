using MapIntegration.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using System.Linq;
using Xamarin.Forms;
using Plugin.Geolocator;
using System.IO;
using Android.Widget;
using Android;

namespace MapIntegration.ViewModels
{
    public class CovidTestingCentersViewModel : INotifyPropertyChanged
    {

        #region Constructor

        public CovidTestingCentersViewModel()
        {
            GetTestingCentersNearMe();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        public async Task<COVIDDetails> GetCovidDataAsync()
        {
            WebClient webClient = new WebClient();
            var result = await webClient.DownloadStringTaskAsync(new Uri("https://raw.githubusercontent.com/ReshmaMasudha/MapIntegration/master/COVID_TEST_CENTERS.txt"));
            return JsonConvert.DeserializeObject<COVIDDetails>(result);
        }

        public async void GetTestingCentersNearMe()
        {
            this.CovidDetails = await this.GetCovidDataAsync();

            GetTestCenterDetailsbasedOnLocation();
        }

        public async void GetTestCenterDetailsbasedOnLocation()
        {
            if (!CrossGeolocator.Current.IsGeolocationEnabled || !CrossGeolocator.Current.IsGeolocationAvailable || !CrossGeolocator.IsSupported)   //current-single ton approach
            {
                this.TestCenterDetails = null;
                return;
            }
            var filteredCenters = new List<TestCenterDetail>();
            var currentlocation = await Geolocation.GetLastKnownLocationAsync();
            if (currentlocation == null)
            {
                currentlocation = await Geolocation.GetLocationAsync(new GeolocationRequest()
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });
            }
            if (currentlocation != null)
            {
                foreach (var item in this.CovidDetails.items)
                {
                    if (Math.Round(Location.CalculateDistance(currentlocation.Latitude, currentlocation.Longitude, item.lat, item.lng, DistanceUnits.Kilometers)) <= 10)
                    {
                        filteredCenters.Add(item);
                    }
                }
                this.TestCenterDetails = filteredCenters;
            }
        }

        public ICommand PerformSearch => new Command<string>((string query) =>
        {
            if (CovidDetails != null)
            {
                if (string.IsNullOrEmpty(query) || (string.IsNullOrWhiteSpace(query)))
                {
                    GetTestCenterDetailsbasedOnLocation();
                }
                else
                {
                    query = query.ToLower();
                    this.TestCenterDetails = this.CovidDetails.items.Where(x => x.address.Split(' ').Any(y => y.ToLower().StartsWith(query))).ToList();
                }
            }
        });
    

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "" )
        {
            if(this.PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }



        #endregion

        #region Properties

        private List<TestCenterDetail> testCenterDetails;
        public List<TestCenterDetail> TestCenterDetails
        {
            get { return testCenterDetails; }
            set
            {
                if(value != testCenterDetails)
                {
                    testCenterDetails = value;
                    RaisePropertyChanged();
                }
            }
        }
        
        public COVIDDetails CovidDetails { get; set; }

        #endregion
       
    }
}
