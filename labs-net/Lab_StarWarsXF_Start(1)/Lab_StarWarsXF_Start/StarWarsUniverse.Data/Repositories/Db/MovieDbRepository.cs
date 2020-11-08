using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StarWarsUniverse.Domain;

namespace StarWarsUniverse.Data.Repositories.Db
{
    public class MovieDbRepository : IMovieRepository
    {
        private readonly StarWarsContext _context;

        public MovieDbRepository(StarWarsContext context)
        {
            _context = context;
        }

        public IList<Movie> GetAllMovies()
        {
            return _context.Movies
                        .Include(m => m.MoviePlanets)
                        .ThenInclude(mp => mp.Planet)
                        .OrderBy(m => m.EpisodeId).ToList(); //Eagerly load planets, lazy loading does not exist in EF Core
        }
    }
}
