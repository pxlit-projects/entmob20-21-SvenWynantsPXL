using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StarWarsUniverse.Data;
using StarWarsUniverse.Data.Repositories.Db;

namespace StarWarsUniverse.Tests
{
    [TestFixture]
    public class MovieDbRepositoryTests
    {
        protected static SqliteConnection Connection;
        protected static StarWarsContext Context;

        [SetUp]
        public void BeforeEachTest()
        {
            Connection = new SqliteConnection("Datasource=:memory:");
            Connection.Open();
            Context = CreateContext();
            Context.Database.EnsureCreated();
        }

        [TearDown]
        public void AfterEachTest()
        {
            Context?.Dispose();
            Connection?.Close();
        }

        protected StarWarsContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<StarWarsContext>()
                .UseSqlite(Connection)
                .EnableSensitiveDataLogging()
                .Options;

            return new StarWarsContext(options);
        }

        [Test]
        public void GetAllMoviesShouldReturnAllMoviesWithPlanetsIncluded()
        {
            var repo = new MovieDbRepository(Context);

            var movies = repo.GetAllMovies();

            foreach (var movie in movies)
            {
                foreach (var moviePlanet in movie.MoviePlanets)
                {
                    Assert.That(moviePlanet.Planet, Is.Not.Null);
                }
            }
        }
    }
}
