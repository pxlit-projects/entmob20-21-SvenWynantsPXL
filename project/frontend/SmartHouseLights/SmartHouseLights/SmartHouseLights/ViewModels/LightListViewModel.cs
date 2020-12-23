using System.Collections.Generic;
using System.Windows.Input;
using SmartHouseLights.Models;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Util;
using SmartHouseLights.Views;
using Xamarin.Forms;

namespace SmartHouseLights.ViewModels
{
    public class LightListViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ILightService _lightService;
        public Command LightSelectedCommand => new Command<Light>(OnLightSelected);

        private Command _flipSwitchCommand;
        public Command FlipSwitchCommand => _flipSwitchCommand ??
                                            (_flipSwitchCommand = new Command(OnFlipPressed, CanFlipSwitch));
        public List<Light> Lights { get; set; }

        public LightListViewModel(INavigationService navigationService, ILightService lightService)
        {
            _navigationService = navigationService;
            _lightService = lightService;
            Title = "Lights";
            Lights = lightService.GetAllLights();
            RefreshCanExecutes();
        }

        private void OnLightSelected(Light light)
        {
            _navigationService.NavigateToAsync(nameof(LightDetailsView));
            MessagingCenter.Instance.Send(this, MessageConstants.LightSelected, light);
        }

        private void OnFlipPressed()
        {
            //Light light = _lightService.FlipSwitch(light);
        }

        private bool CanFlipSwitch()
        {
            if (Lights != null || Lights.Count > 0)
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