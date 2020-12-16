using SmartHouseLights.Services;
using SmartHouseLights.Views;
using System;
using SmartHouseLights.Util;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouseLights
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            AppContainer.RegisterDependencies();
            
            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
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
