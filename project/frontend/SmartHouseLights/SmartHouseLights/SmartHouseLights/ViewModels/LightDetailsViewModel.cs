using SmartHouseLights.Models;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Util;
using SmartHouseLights.Views;
using Xamarin.Forms;

namespace SmartHouseLights.ViewModels
{
    public class LightDetailsViewModel : ViewModelBase
    {
        private readonly ILightService _lightService;
        private readonly INavigationService _navigationService;

        public Command FlipSwitchCommand => new Command(OnFlipSwitch);
        public Command DeleteLightCommand => new Command(OnDelete);
        public Command OnDragCompletedCommand => new Command(OnDragCompleted, CanChangeBrightness);

        public string ErrorMessage { get; set; }

        private Light _light;
        public Light Light
        {
            get => _light;
            set
            {
                if (_light == value) return;
                _light = value;
                OnPropertyChanged();
            }
        }

        public LightDetailsViewModel(ILightService lightService, INavigationService navigationService)
        {
            _lightService = lightService;
            _navigationService = navigationService;
            MessagingCenter.Instance.Subscribe<LightListViewModel, Light>(this, MessageConstants.LightSelected,
                (sender, light) => 
                {
                    Light = light;
                    Title = Light.Name;
                    RefreshCanExecutes();
                });
        }

        private void OnFlipSwitch()
        {
            Light = _lightService.FlipSwitch(Light.Id);
            RefreshCanExecutes();
        }

        private async void OnDelete()
        {
            var action = await Shell.Current.DisplayAlert("Delete light", "Are you sure you want to delete this light?",
                "Yes", "No");

            if (action)
            {
                bool success = _lightService.DeleteLightById(Light.Id);
                
                if (success)
                {
                    ErrorMessage = "";
                    await _navigationService.NavigateToAsync($"..");
                }
                else
                {
                    ErrorMessage = "Something went wrong deleting the light";
                }
            }
        }

        private void OnDragCompleted()
        {
            Light = _lightService.UpdateLight(Light);
            RefreshCanExecutes();
        }

        private bool CanChangeBrightness()
        {
            if (Light != null && Light.OnState)
            {
                return true;
            }

            return false;
        }

        private void RefreshCanExecutes()
        {
            OnDragCompletedCommand.ChangeCanExecute();
        }
    }
}