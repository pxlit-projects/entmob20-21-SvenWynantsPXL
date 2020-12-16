using System.Collections.Generic;
using SmartHouseLights.Models;
using SmartHouseLights.Services;
using Xamarin.Forms;

namespace SmartHouseLights.ViewModels
{
    public class LightListViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IHttpService _httpService;
        public Command LightSelectedCommand => new Command<Light>(OnLightSelected);
        public List<Light> Lights { get; set; }

        public LightListViewModel(INavigationService navigationService, IHttpService httpService)
        {
            _navigationService = navigationService;
            _httpService = httpService;
            Title = "Lights";
            Lights = _httpService.GetAllLights("sven", "pxl");
        }

        private void OnLightSelected(Light light)
        {
            _navigationService.NavigateToAsync("");
        }
    }
}