using System.Collections.Generic;
using StarWarsUniverse.Data;
using StarWarsUniverse.Data.Repositories;
using StarWarsUniverse.Data.Repositories.Db;
using StarWarsUniverse.Domain;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StarWarsXF
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieListView : ContentPage
    {
        public ListView MyListView { get; private set; }

        public MovieListView()
        {
            InitializeComponent();

            IMovieRepository repository = new MovieDbRepository(StarWarsContextFactory.Create());
            IList<Movie> movieList = repository.GetAllMovies();
            SWMoviesListView.ItemsSource = movieList;

            MyListView = SWMoviesListView;
        }
    }
}