using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MapIntegration.ViewModels;

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
            if (e.OldTextValue?.Length > 0 && string.IsNullOrEmpty(e.NewTextValue) && viewModel.PerformSearch.CanExecute(null))
                viewModel.PerformSearch.Execute(null);
        }
    }
}