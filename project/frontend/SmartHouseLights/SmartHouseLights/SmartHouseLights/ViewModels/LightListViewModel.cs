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
        public Command LightSelectedCommand => new Command<Light>(OnLightSelected);
        public List<Light> Lights { get; set; }

        public LightListViewModel(INavigationService navigationService, IHttpService httpService)
        {
            _navigationService = navigationService;
            Title = "Lights";
            Lights = httpService.GetAllLights();
        }

        private void OnLightSelected(Light light)
        {
            MessagingCenter.Instance.Send(this, MessageConstants.LightSelected, light);
            _navigationService.NavigateToAsync(nameof(LightDetailsView));
        }
    }
}