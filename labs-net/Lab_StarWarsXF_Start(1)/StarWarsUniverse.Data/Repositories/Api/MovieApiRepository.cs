using System.Collections.Generic;
using StarWarsUniverse.Domain;

namespace StarWarsUniverse.Data.Repositories.Api
{
    public class MovieApiRepository : ApiRepositoryBase, IMovieRepository
    {
        public IList<Movie> GetAllMovies()
        {
            return GetAllStarWarsResources<Movie>("/api/films/");
        }
    }
}
