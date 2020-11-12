using System.Collections.Generic;
using System.Linq;
using StarWarsUniverse.Domain;
using StarWarsXF.Services;
using StarWarsXF.Util;
using StarWarsXF.Views;
using Xamarin.Forms;

namespace StarWarsXF.ViewModels
{
    public class MovieDetailsViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private Movie _currentMovie;

        private Command _rateDownCommand;

        public Command RateDownCommand =>
            _rateDownCommand ?? (_rateDownCommand = new Command(OnRateDown, OnCanExecuteRateDown));

        private Command _rateUpCommand;

        public Command RateUpCommand => _rateUpCommand ?? (_rateUpCommand = new Command(OnRateUp, OnCanExecuteRateUp));

        public Command ShowPlanetsCommand => new Command(OnShowPlanets);

        public MovieDetailsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            MessagingCenter.Instance.Subscribe<MovieListViewModel, Movie>(this, MessageConstants.MovieSelected,
                (sender, movie) => { CurrentMovie = movie; RefreshCanExecutes(); });

        }

        public Movie CurrentMovie
        {
            get => _currentMovie;
            set
            {
                if (_currentMovie == value) return;
                _currentMovie = value;
                OnPropertyChanged();
            }
        }

        public bool OnCanExecuteRateUp()
        {
            if (CurrentMovie != null && CurrentMovie.Rating < 10)
            {
                return true;
            }

            return false;
        }

        public bool OnCanExecuteRateDown()
        {
            if (CurrentMovie != null && CurrentMovie.Rating > 0)
            {
                return true;
            }

            return false;
        }

        private void OnRateUp()
        {
            CurrentMovie.Rating += 1;
            RefreshCanExecutes();
        }

        private void OnRateDown()
        {
            CurrentMovie.Rating -= 1;
            RefreshCanExecutes();
        }

        private void RefreshCanExecutes()
        {
            (RateDownCommand as Command).ChangeCanExecute();
            (RateUpCommand as Command).ChangeCanExecute();
        }

        private async void OnShowPlanets()
        {
            string title = $"{_currentMovie.Title} - planets";
            await _navigationService.NavigateToAsync<PlanetsViewModel>();

            IList<Planet> planets = CurrentMovie.MoviePlanets.Select(mp => mp.Planet).ToList();
            MessagingCenter.Instance.Send(this, MessageConstants.ShowMoviePlanets, planets);
        }
    }
}