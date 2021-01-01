using System.ComponentModel;
using System.Runtime.CompilerServices;
using SmartHouseLights.Annotations;

namespace SmartHouseLights.Models
{
    public class Light : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
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

        public string Type { get; set; }

        private bool _onState;

        public bool OnState
        {
            get => _onState;
            set
            {
                _onState = value;
                OnPropertyChanged();
            }
        }

        public Manufacturer Manufacturer { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}