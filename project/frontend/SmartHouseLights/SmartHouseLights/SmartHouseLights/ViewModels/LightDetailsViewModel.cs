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

        public Command FlipSwitchCommand => new Command(OnFlipSwitch, OnCanFlip);
        public Command DeleteLightCommand => new Command(OnDelete);
        public Command UpdateLightCommand => new Command(OnUpdate);
        public Command RemoveTimerCommand => new Command(execute: () => { Light.OnTimer = ""; OnUpdate(); });
        public Command AddLightToGroupCommand => new Command(OnAddToGroup);

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
        public User User { get; set; }
        public List<LightGroup> Groups { get; set; }

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

        private bool OnCanFlip()
        {
            if (User.Groups == null || User.Groups.Count < 1)
            {
                ErrorMessage = "";
                return true;
            }

            foreach (var lightGroup in User.Groups)
            {
                if (lightGroup.Id == Light.GroupId)
                {
                    ErrorMessage = "You may not access this group";
                    return false;
                }
            }

            ErrorMessage = "";
            return true;
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