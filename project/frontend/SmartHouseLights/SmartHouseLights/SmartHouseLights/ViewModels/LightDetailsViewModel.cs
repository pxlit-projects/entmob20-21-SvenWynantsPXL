using SmartHouseLights.Models;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Util;
using Xamarin.Forms;

namespace SmartHouseLights.ViewModels
{
    public class LightDetailsViewModel : ViewModelBase
    {
        private readonly ILightService _lightService;
        private readonly IAuthenticationService _authService;

        private Command _flipSwitchCommand;

        public Command FlipSwitchCommand => _flipSwitchCommand ??
                                            (_flipSwitchCommand = new Command(OnFlipSwitch, OnCanExecuteFlipSwitch));

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

        public LightDetailsViewModel(ILightService lightService, IAuthenticationService authService)
        {
            _lightService = lightService;
            _authService = authService;
            MessagingCenter.Instance.Subscribe<LightListViewModel, Light>(this, MessageConstants.LightSelected,
                (sender, light) => { Light = light; RefreshCanExecutes(); });
        }

        private void OnFlipSwitch()
        {
            Light = _lightService.FlipSwitch(_light.Id);
            RefreshCanExecutes();
        }

        private bool OnCanExecuteFlipSwitch()
        {
            if (Light != null)
            {
                return true;
            }

            return false;
        }

        private void RefreshCanExecutes()
        {
            (FlipSwitchCommand as Command).ChangeCanExecute();
        }
    }
}