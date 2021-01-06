using System.ComponentModel;
using System.Runtime.CompilerServices;
using SmartHouseLights.Annotations;

namespace SmartHouseLights.Models
{
    public class CreateLightModel : INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _type;
        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged();
            }
        }

        private Manufacturer _manufacturer;
        public Manufacturer LightManufacturer
        {
            get => _manufacturer;
            set
            {
                _manufacturer = value;
                OnPropertyChanged();
            }
        }

        private int _groupId;
        public int LightGroupId
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