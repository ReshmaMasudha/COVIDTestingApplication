using MapIntegration.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MapIntegration
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new CovidTestingCenterPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
