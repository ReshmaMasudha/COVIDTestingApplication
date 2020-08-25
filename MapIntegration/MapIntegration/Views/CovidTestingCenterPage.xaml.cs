using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MapIntegration.ViewModels;
using System.Globalization;

namespace MapIntegration.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CovidTestingCenterPage : ContentPage
    {
        public CovidTestingCenterPage()
        {
            InitializeComponent();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
               
                viewModel.PerformSearch.Execute(e.NewTextValue);
                CovidCentersList.IsVisible = false;
                locationLabel.IsVisible = false;
                AutocompleteTextView.IsVisible = true;
                
            }
            
        }

        private void AutocompleteTextView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.SelectedItem.ToString()))
            {

                AutocompleteTextView.IsVisible = false;
                CovidCentersList.IsVisible = true;
            }
            
        }
    }
}