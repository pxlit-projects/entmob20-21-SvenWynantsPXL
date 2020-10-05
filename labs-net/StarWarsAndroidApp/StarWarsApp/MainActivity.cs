using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Java.Util;
using Microsoft.EntityFrameworkCore;
using StarWarsUniverse.Data;
using StarWarsUniverse.Data.Repositories.Db;
using StarWarsUniverse.Domain;
using Environment = System.Environment;
using Path = System.IO.Path;

namespace StarWarsApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        private StarWarsContext _context;
        private TextView _infoTextView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            
            _infoTextView = FindViewById<TextView>(Resource.Id.infoTextView);
            Button countButton = FindViewById<Button>(Resource.Id.countButton);

            InitializeDbContextAsync();
            countButton.Enabled = true;

            countButton.Click += (sender, e) =>
            {
                MovieDbRepository repo = new MovieDbRepository(_context);
                IList<Movie> movies = repo.GetAllMovies();
                _infoTextView.Text = $"Found { movies.Count } movies.";
            };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private Task InitializeDbContextAsync()
        {
            _infoTextView.Text = "Initializing database...";
            return Task.Factory.StartNew(() =>
            {
                string dbName =
                    Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "starwars.db");

                string connectionString = $"Data Source = {dbName}";

                var dbContextOptions = new DbContextOptionsBuilder<StarWarsContext>().UseSqlite(connectionString);

                _context = new StarWarsContext(dbContextOptions.Options);

                _infoTextView.Text = "Migrating database...";
                
                _context.Database.Migrate();
                _infoTextView.Text = "Database initialized";
            });
        }
    }
}