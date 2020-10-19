using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StarWarsUniverse.Domain;

namespace StarWarsUniverse.Data.Repositories.Db
{
    public class PlanetDbRepository : IPlanetRepository
    {
        private readonly StarWarsContext _context;

        public PlanetDbRepository(StarWarsContext context)
        {
            _context = context;
        }

        public IList<Planet> GetAllPlanets()
        {
            return _context.Planets.Include(p => p.MoviePlanets).ToList();
        }
    }
}