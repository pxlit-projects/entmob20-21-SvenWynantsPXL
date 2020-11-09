using System.Collections.Generic;
using StarWarsUniverse.Domain;

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

    }
}