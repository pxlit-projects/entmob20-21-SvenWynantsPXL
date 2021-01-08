using System.ComponentModel;
using System.Runtime.CompilerServices;
using SmartHouseLights.Annotations;

namespace SmartHouseLights.Models
{
    public class CreateGroupModel : INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}