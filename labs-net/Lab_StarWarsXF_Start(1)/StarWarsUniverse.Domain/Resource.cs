using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using StarWarsUniverse.Domain.Annotations;

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
