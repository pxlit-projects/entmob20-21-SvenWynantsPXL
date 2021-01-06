using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SmartHouseLights.Models;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Views;
using Xamarin.Forms;

namespace SmartHouseLights.ViewModels
{
    public class AddLightViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ILightService _lightService;
        
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
        public List<LightGroup> Groups { get; set; }

        public AddLightViewModel(INavigationService navigationService, ILightService lightService, IGroupService groupService)
        {
            _navigationService = navigationService;
            _lightService = lightService;

            Groups = groupService.GetAllGroups();

            Title = "Add a light";
        }

        public void OnSaveLightAsync()
        {
            if (LightModel.Name != null && !LightModel.Name.Equals("") && LightModel.Type != null && !LightModel.Type.Equals(""))
            {
                Light light = _lightService.AddLight(LightModel);
            }
            _navigationService.NavigateToAsync($"//{nameof(LightListView)}");
        }
    }
}