namespace StarWarsUniverse.Domain
{
    public class MoviePlanet
    {
        public string MovieUri { get; set; }
        public Movie Movie { get; set; }
        public string PlanetUri { get; set; }
        public Planet Planet { get; set; }
    }
}
