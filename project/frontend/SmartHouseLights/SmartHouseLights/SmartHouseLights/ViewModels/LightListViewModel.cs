using System.Collections.Generic;
using SmartHouseLights.Models;

namespace SmartHouseLights.ViewModels
{
    public class LightListViewModel : ViewModelBase
    {
        public List<Light> Lights { get; set; }

        public LightListViewModel()
        {
            Title = "Lights";
        }
    }
}