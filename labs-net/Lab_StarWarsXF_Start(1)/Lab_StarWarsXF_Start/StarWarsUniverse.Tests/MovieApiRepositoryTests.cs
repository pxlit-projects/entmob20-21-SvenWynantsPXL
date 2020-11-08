using NUnit.Framework;
using StarWarsUniverse.Data.Repositories.Api;

namespace StarWarsUniverse.Tests
{
    [TestFixture]
    public class MovieApiRepositoryTests
    {
        private MovieApiRepository _repo;

        [SetUp]
        public void SetUp()
        {
            _repo = new MovieApiRepository();
        }

        [Test]
        public void GetAllMoviesShouldReturnEveryMovie()
        {
            //Act
            var returnedMovies = _repo.GetAllMovies();

            //Assert
            Assert.That(returnedMovies, Has.Count.GreaterThanOrEqualTo(6));
        }
    }
}
