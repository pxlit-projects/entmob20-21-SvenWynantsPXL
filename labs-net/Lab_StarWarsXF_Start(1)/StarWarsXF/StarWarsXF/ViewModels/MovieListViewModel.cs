using System.Collections;
using System.Collections.Generic;
using StarWarsUniverse.Data;
using StarWarsUniverse.Data.Repositories;
using StarWarsUniverse.Data.Repositories.Db;
using StarWarsUniverse.Domain;
using StarWarsXF.Views;
using Xamarin.Forms;

namespace StarWarsXF.ViewModels
{
    public class MovieListViewModel : ViewModelBase
    {
        public IList<Movie> Movies { get; set; }

        public Command MovieSelectedCommand => new Command<Movie>(OnMovieSelected);

        public MovieListViewModel()
        {
            IMovieRepository repository = new MovieDbRepository(StarWarsContextFactory.Create());
            Movies = repository.GetAllMovies();
        }

        private async void OnMovieSelected(Movie movie)
        {
            //Navigate to the detail view
            var mainView = (MainView) Application.Current.MainPage;
            var detailNavigationPage = (NavigationPage) mainView.Detail;

            var movieDetailsView = detailNavigationPage.CurrentPage as MovieDetailsView;

            if (movieDetailsView == null)
            {
                movieDetailsView = new MovieDetailsView
                {
                    BindingContext = new MovieDetailsViewModel()
                };
                await detailNavigationPage.PushAsync(movieDetailsView);
            }

            //Set the selected movie
            var viewModel = (MovieDetailsViewModel) movieDetailsView.BindingContext;
            viewModel.CurrentMovie = movie;

            //Hide master page
            mainView.IsPresented = false;
        }
    }
}