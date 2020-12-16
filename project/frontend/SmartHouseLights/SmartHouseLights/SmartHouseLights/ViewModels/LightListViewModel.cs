using System.Collections.Generic;
using SmartHouseLights.Models;
using SmartHouseLights.Services;

namespace SmartHouseLights.ViewModels
{
    public class LightListViewModel : ViewModelBase
    {
        private readonly INavigationService _service;
        public List<Light> Lights { get; set; }

        public LightListViewModel(INavigationService service)
        {
            _service = service;
            Title = "Lights";
        }
    }
}