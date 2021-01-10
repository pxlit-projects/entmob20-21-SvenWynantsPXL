using System.IO;
using SmartHouseLights.Data;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Util;
using Xamarin.Forms;

namespace SmartHouseLights
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            string dbName = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "smarthouse.db");
            SmartHouseContextFactory.ConnectionString = $"Data Source = {dbName}";

            AppContainer.RegisterDependencies();
            
            MainPage = new AppShell(AppContainer.Resolve<INavigationService>(), AppContainer.Resolve<IConnectionFactory>());
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
