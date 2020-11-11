using System.Collections.Generic;
using StarWarsUniverse.Domain;
using StarWarsXF.Util;
using Xamarin.Forms;

namespace StarWarsXF.ViewModels
{
    public class PlanetsViewModel : ViewModelBase
    {
        private IList<Planet> _planets;

        public IList<Planet> Planets
        {
            get => _planets;
            set
            {
                _planets = value;
                OnPropertyChanged();
            }
        }

        public PlanetsViewModel()
        {
            MessagingCenter.Instance.Subscribe<MovieDetailsViewModel, IList<Planet>>(this, MessageConstants.ShowMoviePlanets,
                (sender, planets) => { Planets = planets; });
        }

    }
}