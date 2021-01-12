using System.Collections.Generic;
using SmartHouseLights.Domain.Models;
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
        
        public Command SaveLightCommand => new Command(OnSaveLight);

        private LightGroup _lightGroup;
        public LightGroup CurrentGroup
        {
            get => _lightGroup;
            set
            {
                _lightGroup = value;
                OnPropertyChanged();
            }
        }

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

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public AddLightViewModel(INavigationService navigationService, ILightService lightService, IGroupService groupService)
        {
            _navigationService = navigationService;
            _lightService = lightService;
            LightModel = new CreateLightModel();
            Groups = new List<LightGroup>
            {
                CreateEmptyGroup()
            };
            Groups.AddRange(groupService.GetAllGroups());
        }

        public void OnSaveLight()
        {
            if (LightModel.Name != null && !LightModel.Name.Equals("") && LightModel.Type != null && !LightModel.Type.Equals(""))
            {
                if (CurrentGroup != null)
                {
                    LightModel.LightGroupId = CurrentGroup.Id;
                }

                ErrorMessage = "";
                Light light = _lightService.AddLight(LightModel);
                _navigationService.NavigateToAsync($"//{nameof(LightListView)}");
            }
            else
            {
                ErrorMessage = "All fields need to be entered.";
            }
        }

        private LightGroup CreateEmptyGroup()
        {
            LightGroup group = new LightGroup
            {
                Id = 0,
                Name = "No Group"
            };
            return group;
        }
    }
}