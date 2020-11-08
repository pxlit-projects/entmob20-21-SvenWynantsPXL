using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StarWarsUniverse.Data;

namespace StarWarsUniverse.Tests
{
    public class StarWarsDbTestBase
    {
        protected static SqliteConnection Connection;
        protected static StarWarsContext Context;

        [SetUp]
        public void BeforeEachTest()
        {
            Connection = new SqliteConnection("DataSource=:memory:");
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
    }
}
