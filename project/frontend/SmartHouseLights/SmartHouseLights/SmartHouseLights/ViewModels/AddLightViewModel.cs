using SmartHouseLights.Models;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Views;
using Xamarin.Forms;

namespace SmartHouseLights.ViewModels
{
    public class AddLightViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public Command SaveLightCommand => new Command(OnSaveLightAsync);

        private CreateLightModel _lightModel;
        public CreateLightModel LightModel
        {
            get => _lightModel;
            set
            {
                _lightModel = value;
                OnPropertyChanged();
            }
        }

        public AddLightViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Title = "Add a light";
        }

        public void OnSaveLightAsync()
        {
            _navigationService.NavigateToAsync($"//{nameof(LightListView)}");
        }
    }
}