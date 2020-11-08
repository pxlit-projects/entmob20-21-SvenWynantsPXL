using Microsoft.EntityFrameworkCore;
using StarWarsUniverse.Data.Repositories.Api;
using StarWarsUniverse.Domain;
using System.Collections.Generic;
using System.Linq;

namespace StarWarsUniverse.Data
{
    public class StarWarsContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Planet> Planets { get; set; }

        public StarWarsContext() { }

        public StarWarsContext(DbContextOptions<StarWarsContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source = starwars.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define Primary Keys
            modelBuilder.Entity<Movie>().HasKey(movie => movie.Uri);
            modelBuilder.Entity<Planet>().HasKey(planet => planet.Uri);

            // Don't map lists of Uri, this is modeled in MoviePlanet entity
            modelBuilder.Entity<Movie>().Ignore(movie => movie.PlanetUris);
            modelBuilder.Entity<Planet>().Ignore(planet => planet.MovieUris);

            // Configure many to many
            modelBuilder.Entity<MoviePlanet>().HasKey(moviePlanet => new
            {
                moviePlanet.MovieUri,
                moviePlanet.PlanetUri
            });

            // Seed Data
            IList<Movie> remoteMovies = new MovieApiRepository().GetAllMovies();
            modelBuilder.Entity<Movie>().HasData(remoteMovies.ToArray());

            IList<Planet> remotePlanets = new PlanetApiRepository().GetAllPlanets();
            modelBuilder.Entity<Planet>().HasData(remotePlanets.ToArray());

            foreach (var remotePlanet in remotePlanets)
            {
                foreach (var remotePlanetMovieUri in remotePlanet.MovieUris)
                {
                    modelBuilder.Entity<MoviePlanet>().HasData(
                        new MoviePlanet
                        {
                            MovieUri = remotePlanetMovieUri,
                            PlanetUri = remotePlanet.Uri
                        });
                }
            }
        }
    }
}
