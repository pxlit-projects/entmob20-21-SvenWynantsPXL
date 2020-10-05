using System;
using System.Collections.Generic;
using System.Text;
using StarWarsUniverse.Domain;

namespace StarWarsUniverse.Data.Repositories
{
    public interface IPlanetRepository
    {
        IList<Planet> GetAllPlanets();
    }
}
