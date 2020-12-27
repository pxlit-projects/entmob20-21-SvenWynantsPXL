using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SmartHouseLights.Annotations;

namespace SmartHouseLights.Models
{
    public class LightGroup : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private bool _hasOnState;
        public bool HasOnState
        {
            get => _hasOnState;
            set
            {
                _hasOnState = value;
                OnPropertyChanged();
            }
        }

        private bool _allOnState;

        public bool AllOnState
        {
            get => _allOnState;
            set
            {
                _allOnState = value;
                OnPropertyChanged();
            }
        }

        private int _brightness;
        public int Brightness
        {
            get => _brightness;
            set
            {
                _brightness = value;
                OnPropertyChanged();
            }
        }

        public List<Light> Lights { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}