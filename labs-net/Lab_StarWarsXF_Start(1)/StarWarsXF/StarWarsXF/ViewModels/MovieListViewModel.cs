using System.Collections;
using System.Collections.Generic;
using StarWarsUniverse.Data;
using StarWarsUniverse.Data.Repositories;
using StarWarsUniverse.Data.Repositories.Db;
using StarWarsUniverse.Domain;
using StarWarsXF.Services;
using StarWarsXF.Util;
using StarWarsXF.Views;
using Xamarin.Forms;

namespace StarWarsXF.ViewModels
{
    public class MovieListViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public IList<Movie> Movies { get; set; }

        public Command MovieSelectedCommand => new Command<Movie>(OnMovieSelected);

        public MovieListViewModel(IMovieRepository movieRepository, INavigationService navigationService)
        {
            _navigationService = navigationService;
            
            Movies = movieRepository.GetAllMovies();

        }

        private async void OnMovieSelected(Movie movie)
        {
            await _navigationService.NavigateToAsync<MovieDetailsViewModel>();
            
            MessagingCenter.Instance.Send(this, MessageConstants.MovieSelected, movie);
        }
    }
}