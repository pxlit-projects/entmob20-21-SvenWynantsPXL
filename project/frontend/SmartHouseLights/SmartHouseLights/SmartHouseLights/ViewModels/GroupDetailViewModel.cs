using System.Linq;
using Newtonsoft.Json.Serialization;
using SmartHouseLights.Domain.Models;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Util;
using SmartHouseLights.Views;
using Xamarin.Forms;

namespace SmartHouseLights.ViewModels
{
    public class GroupDetailViewModel : ViewModelBase
    {
        private readonly IGroupService _groupService;
        private readonly INavigationService _navigationService;
        private readonly IAuthenticationService _authService;

        private Command _flipSwitchCommand;
        public Command FlipSwitchCommand =>
            _flipSwitchCommand ??= new Command(OnFlipSwitch, OnCanFlipSwitch);

        public Command DeleteGroupCommand => new Command(OnDelete);

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

        private LightGroup _group;
        public LightGroup Group
        {
            get => _group;
            set
            {
                _group = value;
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

        public GroupDetailViewModel(IGroupService groupService, INavigationService navigationService,
            IAuthenticationService authService)
        {
            _groupService = groupService;
            _navigationService = navigationService;
            _authService = authService;
            ErrorMessage = "";
            MessagingCenter.Instance.Subscribe<GroupListViewModel, LightGroup>(this, MessageConstants.GroupSelected,
                (sender, group) =>
                {
                    Group = group;
                    Title = $"Group: {Group.Name}";
                    User = _authService.GetUser();
                    RefreshCanExecutes();
                });
        }

        private void OnFlipSwitch()
        {
            if (Group.AllOnState)
            {
                _groupService.TurnAllLightsOffInGroup(Group.Id);
                Group.AllOnState = false;
                Group.HasOnState = false;
                foreach (var light in Group.Lights)
                {
                    light.OnState = false;
                }
            }
            else
            {
                _groupService.TurnAllLightsOnInGroup(Group.Id);
                Group.AllOnState = true;
                Group.HasOnState = true;
                foreach (var light in Group.Lights)
                {
                    light.OnState = true;
                }
            }
        }

        public bool OnCanFlipSwitch()
        {
            if (Group?.Lights?.Count > 0)
            {
                if (User.Groups == null || User.Groups.Count < 1)
                {
                    return true;
                }

                return User.Groups.All(lightGroup => lightGroup.Id != Group.Id);
            }

            return false;
        }

        private async void OnDelete()
        {
            var action = await Shell.Current.DisplayAlert("Delete group", "Are you sure you want to delete this group?",
                "Yes", "No");

            if (action)
            {
                bool success = _groupService.DeleteGroupById(Group.Id);

                if (success)
                {
                    ErrorMessage = "";
                    await _navigationService.NavigateToAsync("..");
                }
                else
                {
                    ErrorMessage = "Something went wrong deleting the group";
                }
            }
        }

        private void RefreshCanExecutes()
        {
            FlipSwitchCommand.ChangeCanExecute();
        }
    }
}