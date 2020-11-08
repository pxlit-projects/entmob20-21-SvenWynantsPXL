using System.IO;
using Microsoft.EntityFrameworkCore;
using StarWarsUniverse.Data;
using Xamarin.Forms;

namespace StarWarsXF
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            string dbName = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "starwars.db");
            StarWarsContextFactory.ConnectionString = $"Data Source = {dbName}";

            using (StarWarsContext context = StarWarsContextFactory.Create())
            {
                context.Database.Migrate();
            };

            MainPage = new MainView();
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
