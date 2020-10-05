using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StarWarsUniverse.Data.Repositories.Api;
using StarWarsUniverse.Domain;

namespace StarWarsUniverse.Data
{
    public class StarWarsContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Planet> Planets { get; set; }

        public StarWarsContext() { }

        public StarWarsContext(DbContextOptions<StarWarsContext> options)
            :base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source = starwars.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Movie>().HasKey(m => m.Uri);
            builder.Entity<Planet>().HasKey(p => p.Uri);
            builder.Entity<MoviePlanet>().HasKey(mp => new
            {
                mp.MovieUri, mp.PlanetUri
            });

            IList<Movie> remoteMovies = new MovieApiRepository().GetAllMovies();
            builder.Entity<Movie>().HasData(remoteMovies.ToArray());

            IList<Planet> remotePlanets = new PlanetApiRepository().GetAllPlanets();
            builder.Entity<Planet>().HasData(remotePlanets.ToArray());

            List<MoviePlanet> movplans = new List<MoviePlanet>();
            foreach (var movie in remoteMovies)
            {
                foreach (var planetUri in movie.PlanetUris)
                {
                    movplans.Add(new MoviePlanet
                    {
                        PlanetUri = planetUri,
                        MovieUri = movie.Uri,
                    });
                }
            }
            builder.Entity<MoviePlanet>().HasData(movplans.ToArray());

            builder.Entity<Movie>().Ignore(m => m.PlanetUris);
            builder.Entity<Planet>().Ignore(p => p.MovieUris);
        }
    }
}