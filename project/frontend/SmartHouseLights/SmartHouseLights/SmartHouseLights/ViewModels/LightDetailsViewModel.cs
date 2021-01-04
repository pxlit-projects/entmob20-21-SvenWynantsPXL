using SmartHouseLights.Models;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Util;
using Xamarin.Forms;

namespace SmartHouseLights.ViewModels
{
    public class LightDetailsViewModel : ViewModelBase
    {
        private readonly ILightService _lightService;

        public Command FlipSwitchCommand => new Command(OnFlipSwitch);

        public Command OnDragCompletedCommand => new Command(OnDragCompleted, CanChangeBrightness);

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

        public LightDetailsViewModel(ILightService lightService)
        {
            _lightService = lightService;
            MessagingCenter.Instance.Subscribe<LightListViewModel, Light>(this, MessageConstants.LightSelected,
                (sender, light) => 
                {
                    Light = light;
                    Title = Light.Name;
                });
        }

        private void OnFlipSwitch()
        {
            Light = _lightService.FlipSwitch(_light.Id);
        }

        private void OnDragCompleted()
        {
            Light = _lightService.UpdateLight(Light);
        }

        private bool CanChangeBrightness()
        {
            if (Light != null)
            {
                if (Light.OnState)
                {
                    return true;
                }
            }

            return false;
        }

        private void RefreshCanExecutes()
        {
            OnDragCompletedCommand.ChangeCanExecute();
        }
    }
}