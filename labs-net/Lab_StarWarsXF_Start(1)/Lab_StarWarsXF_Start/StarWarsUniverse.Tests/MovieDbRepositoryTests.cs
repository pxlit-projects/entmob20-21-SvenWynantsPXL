using System.Linq;
using NUnit.Framework;
using StarWarsUniverse.Data.Repositories.Db;
using StarWarsUniverse.Domain;

namespace StarWarsUniverse.Tests
{
    [TestFixture]
    public class MovieDbRepositoryTests : StarWarsDbTestBase
    {
        [Test]
        public void GetAllMoviesShouldReturnEveryMovie()
        {
            //Arrange
            var repo = new MovieDbRepository(Context);

            //Act
            var returnedMovies = repo.GetAllMovies();

            //Assert
            Assert.That(returnedMovies, Has.Count.GreaterThanOrEqualTo(6));
        }

        [Test]
        public void GetAllMoviesShouldLoadPlanets()
        {
            var repo = new MovieDbRepository(Context);

            //Act
            var allMovies = repo.GetAllMovies();

            //Assert
            Assert.That(allMovies, Has.All.Matches((Movie movie) => movie.MoviePlanets.Count >= 1));
            Assert.That(allMovies, Has.All.Matches((Movie movie) => movie.MoviePlanets.First().Planet != null));
        }
    }
}
