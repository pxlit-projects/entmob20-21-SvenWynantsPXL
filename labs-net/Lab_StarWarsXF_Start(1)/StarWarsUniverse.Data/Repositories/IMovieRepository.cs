using StarWarsUniverse.Domain;
using System.Collections.Generic;

namespace StarWarsUniverse.Data.Repositories
{
    public interface IMovieRepository
    {
        IList<Movie> GetAllMovies();
    }
}
