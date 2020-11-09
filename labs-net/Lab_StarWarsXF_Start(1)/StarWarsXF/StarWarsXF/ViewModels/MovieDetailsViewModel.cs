using System.Linq;
using StarWarsUniverse.Domain;
using StarWarsXF.Views;
using Xamarin.Forms;

namespace StarWarsXF.ViewModels
{
    public class MovieDetailsViewModel : ViewModelBase
    {
        private Movie _currentMovie;

        private Command _rateDownCommand;

        public Command RateDownCommand =>
            _rateDownCommand ?? (_rateDownCommand = new Command(OnRateDown, OnCanExecuteRateDown));

        private Command _rateUpCommand;

        public Command RateUpCommand => _rateUpCommand ?? (_rateUpCommand = new Command(OnRateUp, OnCanExecuteRateUp));

        public Command ShowPlanetsCommand => new Command(OnShowPlanets);

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

        public void RefreshCanExecutes()
        {
            (RateDownCommand as Command).ChangeCanExecute();
            (RateUpCommand as Command).ChangeCanExecute();
            (ShowPlanetsCommand as Command).ChangeCanExecute();
        }

        private async void OnShowPlanets()
        {
            //Navigate to the detail view
            var mainView = (MainView) Application.Current.MainPage;
            var detailNavigationPage = (NavigationPage) mainView.Detail;

            var planetsView = new PlanetsView
            {
                BindingContext = new PlanetsViewModel()
            };

            await detailNavigationPage.PushAsync(planetsView);

            //Set planets
            var viewModel = (PlanetsViewModel) planetsView.BindingContext;

            viewModel.Planets = CurrentMovie.MoviePlanets.Select(mp => mp.Planet).ToList();
        }
    }
}