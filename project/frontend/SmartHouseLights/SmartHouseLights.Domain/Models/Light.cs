using System.ComponentModel;
using System.Runtime.CompilerServices;
using SmartHouseLights.Domain.Annotations;

namespace SmartHouseLights.Domain.Models
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
        private int _groupId;

        public int GroupId
        {
            get => _groupId;
            set
            {
                _groupId = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}