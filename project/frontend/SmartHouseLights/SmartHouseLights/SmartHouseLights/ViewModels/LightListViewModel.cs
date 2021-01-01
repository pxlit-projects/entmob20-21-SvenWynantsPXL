using System.Collections.Generic;
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
        public Command LightSelectedCommand => new Command<int>(OnLightSelected);

        private bool _isRefreshing = false;

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public Command RefreshListCommand => new Command(OnRefreshList);

        private Command _flipSwitchCommand;
        public Command FlipSwitchCommand => _flipSwitchCommand ??
                                            (_flipSwitchCommand = new Command<int>(OnFlipPressed));
        private List<Light> _lights;
        public List<Light> Lights 
        {
            get => _lights;
            set
            {
                _lights = value;
                OnPropertyChanged();
            }
        }

        public LightListViewModel(INavigationService navigationService, ILightService lightService)
        {
            _navigationService = navigationService;
            _lightService = lightService;
            Title = "Lights";
            Lights = lightService.GetAllLights();
        }

        private void OnLightSelected(int lightId)
        {
            MessagingCenter.Instance.Send(this, MessageConstants.LightSelected, Lights[lightId]);
            _navigationService.NavigateToAsync(nameof(LightDetailsView));
        }

        private void OnFlipPressed(int id)
        {
            _lightService.FlipSwitch(id);
            Lights[GetListId(id)].OnState = !Lights[GetListId(id)].OnState;
        }

        private void OnRefreshList()
        {
            IsRefreshing = true;
            Lights = _lightService.GetAllLights();
            IsRefreshing = false;
        }

        private int GetListId(int id)
        {
            for (int i = 0; i < Lights.Count; i++)
            {
                if (Lights[i].Id == id)
                {
                    return i;
                }
            }

            return 0;
        }
    }
}