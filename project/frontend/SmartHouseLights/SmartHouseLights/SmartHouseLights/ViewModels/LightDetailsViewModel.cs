using System;
using System.Collections.Generic;
using SmartHouseLights.Domain.Models;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Util;
using Xamarin.Forms;

namespace SmartHouseLights.ViewModels
{
    public class LightDetailsViewModel : ViewModelBase
    {
        private readonly ILightService _lightService;
        private readonly INavigationService _navigationService;
        private readonly IGroupService _groupService;
        private readonly IAlertService _alertService;

        public Command FlipSwitchCommand => new Command(OnFlipSwitch);
        public Command DeleteLightCommand => new Command(OnDelete);
        public Command UpdateLightCommand => new Command(OnUpdate);
        public Command RemoveTimerCommand => new Command(execute: () => { Light.OnTimer = ""; });
        public Command SaveChangesCommand => new Command(OnUpdate);
        public Command AddLightToGroupCommand => new Command(OnAddToGroup);
        public Command RefreshCommand => new Command(OnRefreshLight);

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

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }

        private LightGroup _currentGroup;
        public LightGroup CurrentGroup
        {
            get => _currentGroup;
            set
            {
                _currentGroup = value;
                OnPropertyChanged();
            }
        }

        private User _user;

        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }
        public List<LightGroup> Groups { get; set; }

        private Light _light;
        public Light Light
        {
            get => _light;
            set
            {
                _light = value;
                OnPropertyChanged();
            }
        }

        public LightDetailsViewModel(ILightService lightService, INavigationService navigationService, IGroupService groupService, IAuthenticationService authService, IAlertService alertService)
        {
            _lightService = lightService;
            _navigationService = navigationService;
            _groupService = groupService;
            _alertService = alertService;
            ErrorMessage = "";
            Groups = new List<LightGroup> {CreateEmptyGroup()};
            Groups.AddRange(groupService.GetAllGroups());
            User = authService.GetUser();
            MessagingCenter.Instance.Subscribe<LightListViewModel, Light>(this, MessageConstants.LightSelected,
                (sender, light) => 
                {
                    Light = light;
                    Title = Light.Name;
                    CurrentGroup = Groups[GetListId(Light.GroupId)];
                    RefreshCanExecutes();
                });
        }

        private void OnFlipSwitch()
        {
            Light = _lightService.FlipSwitch(Light.Id);
            RefreshCanExecutes();
        }

        private async void OnDelete()
        {
            var action = await _alertService.PopupOnDeleteLight();

            if (action)
            {
                bool success = _lightService.DeleteLightById(Light.Id);
                
                if (success)
                {
                    ErrorMessage = "";
                    await _navigationService.NavigateToAsync($"..");
                }
                else
                {
                    ErrorMessage = "Something went wrong deleting the light";
                }
            }
        }

        private void OnUpdate()
        {
            Light = _lightService.UpdateLight(Light);
            RefreshCanExecutes();
        }

        private void OnAddToGroup()
        {
            if (CurrentGroup.Id != 0)
            {
                ErrorMessage = "";
                _groupService.AddLightToGroup(CurrentGroup.Id, Light.Id);
            }
            else
            {
                ErrorMessage = "You cannot add no group!";
            }
        }

        private void OnRefreshLight()
        {
            IsRefreshing = true;
            Light = _lightService.GetLightById(Light.Id);
            RefreshCanExecutes();
            IsRefreshing = false;
        }

        private void RefreshCanExecutes()
        {
            UpdateLightCommand.ChangeCanExecute();
        }

        private LightGroup CreateEmptyGroup()
        {
            var group = new LightGroup
            {
                Name = "No Group",
                Id = 0
            };

            return group;
        }

        private int GetListId(int id)
        {
            for (int i = 0; i < Groups.Count; i++)
            {
                if (Groups[i].Id == id)
                {
                    return i;
                }
            }

            return 0;
        }
    }
}