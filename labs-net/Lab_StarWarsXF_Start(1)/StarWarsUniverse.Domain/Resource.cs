using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using StarWarsUniverse.Domain.Annotations;

namespace StarWarsUniverse.Domain
{
    public abstract class Resource : INotifyPropertyChanged
    {
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Uri { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
