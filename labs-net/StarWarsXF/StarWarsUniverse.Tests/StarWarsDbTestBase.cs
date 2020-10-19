using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StarWarsUniverse.Data;
using StarWarsUniverse.Data.Repositories.Db;
using StarWarsUniverse.Domain;

namespace StarWarsUniverse.Tests
{
    [TestFixture]
    class StarWarsDbTestBase
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
        public void GetAllMoviesShouldReturnEveryMovie()
        {
            var repo = new MovieDbRepository(Context);

            IList<Movie> movies = repo.GetAllMovies();

            Assert.That(movies.Count, Is.EqualTo(6));
        }
    }
}
