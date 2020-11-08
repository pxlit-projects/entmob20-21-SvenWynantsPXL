using StarWarsUniverse.Domain;
using System.Collections.Generic;

namespace StarWarsUniverse.Data.Repositories.Api
{
    public class PlanetApiRepository : ApiRepositoryBase, IPlanetRepository
    {
        public IList<Planet> GetAllPlanets()
        {
            return GetAllStarWarsResources<Planet>("/api/planets/");
        }
    }
}
