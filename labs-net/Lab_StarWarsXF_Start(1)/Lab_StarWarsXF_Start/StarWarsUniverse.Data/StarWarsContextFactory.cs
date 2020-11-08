using Microsoft.EntityFrameworkCore;

namespace StarWarsUniverse.Data
{
    public class StarWarsContextFactory
    {
        public static string ConnectionString;

        public static StarWarsContext Create()
        {
            var options = new DbContextOptionsBuilder<StarWarsContext>()
                .UseSqlite(ConnectionString)
                .Options;
            return new StarWarsContext(options);
        }
    }
}
