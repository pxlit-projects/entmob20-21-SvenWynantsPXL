﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace StarWarsUniverse.Domain
{
    public class Planet : Resource
    {
        public string Name { get; set; }
        
        [JsonProperty("rotation_period")]
        public string RotationPeriod { get; set; }

        [JsonProperty("orbital_period")]
        public string OrbitalPeriod { get; set; }

        public string Diameter { get; set; }

        public string Climate { get; set; }

        public string Gravity { get; set; }

        public string Terrain { get; set; }

        [JsonProperty("surface_water")]
        public string SurfaceWater { get; set; }

        public string Population { get; set; }

        [JsonIgnore]
        public List<MoviePlanet> MoviePlanets { get; set; } = new List<MoviePlanet>();

        [JsonProperty(PropertyName = "films")]
        public List<string> MovieUris { get; set; }
    }
}