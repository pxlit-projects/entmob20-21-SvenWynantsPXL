using System;
using Newtonsoft.Json;

namespace StarWarsUniverse.Domain
{
    public abstract class Resource
    {
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Uri { get; set; }
    }
}
