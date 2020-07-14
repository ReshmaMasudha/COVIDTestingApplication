using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin;
using System.Diagnostics;
using Xamarin.Essentials;
using System.Threading;
using System.Net;
using Newtonsoft.Json;
using MapIntegration.Model;
using MapIntegration.Views;

namespace MapIntegration
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
           // Xamarin.FormsMaps.Init("INSERT_AUTHENTICATION_TOKEN_HERE");
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            WebClient webClient = new WebClient();
            var result =   await webClient.DownloadStringTaskAsync(new Uri("https://raw.githubusercontent.com/ReshmaMasudha/MapIntegration/master/COVID_TEST_CENTERS.txt"));

            var covidDetails =  JsonConvert.DeserializeObject<COVIDDetails>(result);
            //var serialize = JsonConvert.SerializeObject(covidDetails);
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest()
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
                }

                if (location == null)
                    LabelLocation.Text = "No GPS";
                else
                    LabelLocation.Text = $"{location.Latitude} {location.Longitude}";

            }
            catch (Exception ex)
            { 
                Debug.WriteLine($"Something is Wrong: {ex.Message}");
            }

        }

       
    }
}
