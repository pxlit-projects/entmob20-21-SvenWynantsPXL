using System.Collections.Generic;
using NUnit.Framework;
using StarWarsUniverse.Data.Repositories.Api;
using StarWarsUniverse.Domain;

namespace StarWarsUniverse.Tests
{
    [TestFixture]
    public class MovieApiRepositoryTests
    {
        private MovieApiRepository _repo;

        [SetUp]
        public void Setup()
        {
            _repo = new MovieApiRepository();
        }

        [Test]
        public void GetAllMovies()
        {
            IList<Movie> movies = _repo.GetAllMovies();

            Assert.That(movies.Count, Is.GreaterThan(5));
        }
    }
}