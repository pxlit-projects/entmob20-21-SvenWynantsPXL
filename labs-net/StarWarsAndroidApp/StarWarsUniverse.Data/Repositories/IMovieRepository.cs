using System.Collections;
using System.Collections.Generic;
using StarWarsUniverse.Domain;

namespace StarWarsUniverse.Data.Repositories
{
    public interface IMovieRepository
    {
        IList<Movie> GetAllMovies();
    }
}