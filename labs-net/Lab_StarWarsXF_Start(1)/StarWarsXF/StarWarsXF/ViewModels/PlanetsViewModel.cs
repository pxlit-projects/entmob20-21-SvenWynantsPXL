using System.Collections.Generic;
using StarWarsUniverse.Domain;

namespace StarWarsXF.ViewModels
{
    public class PlanetsViewModel : ViewModelBase
    {
        public List<Planet> Planets { get; set; }

    }
}