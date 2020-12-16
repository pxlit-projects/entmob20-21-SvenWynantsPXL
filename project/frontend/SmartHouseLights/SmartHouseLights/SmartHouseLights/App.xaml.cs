using SmartHouseLights.Util;
using Xamarin.Forms;

namespace SmartHouseLights
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            AppContainer.RegisterDependencies();
            
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
