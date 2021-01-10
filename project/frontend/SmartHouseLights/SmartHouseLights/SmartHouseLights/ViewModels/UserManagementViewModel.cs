using System.Collections.Generic;
using System.Linq;
using SmartHouseLights.Domain.Models;
using SmartHouseLights.Services.Interfaces;
using Xamarin.Forms;

namespace SmartHouseLights.ViewModels
{
    public class UserManagementViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly IGroupService _groupService;
        private User _user;
        public User CurrentUser
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged();
                FillLists();
            }
        }

        private List<User> _users;
        public List<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        private List<LightGroup> _enabledGroups;
        public List<LightGroup> EnabledGroups
        {
            get => _enabledGroups;
            set
            {
                _enabledGroups = value;
                OnPropertyChanged();
            }
        }

        private List<LightGroup> _disabledGroups;
        public List<LightGroup> DisabledGroups
        {
            get => _disabledGroups;
            set
            {
                _disabledGroups = value;
                OnPropertyChanged();
            }
        }

        public List<LightGroup> Groups { get; set; }

        public Command DisableGroupCommand => new Command<int>(OnDisableGroup, OnUserSelected);
        public Command EnableGroupCommand => new Command<int>(OnEnableGroup, OnUserSelected);
        public UserManagementViewModel(IUserService userService, IGroupService groupService)
        {
            _userService = userService;
            _groupService = groupService;
            Users = userService.GetAllUsers();
            Groups = groupService.GetAllGroups();
            Title = "UserManagement";
        }

        private void OnDisableGroup(int groupId)
        {
            _userService.RestrictUserForGroup(CurrentUser.Id, groupId);
            FillLists();
        }

        private void OnEnableGroup(int groupId)
        {
            _userService.RemoveRestriction(CurrentUser.Id, groupId);
            FillLists();
        }

        private bool OnUserSelected(int groupId)
        {
            if (CurrentUser != null)
            {
                return true;
            }

            return false;
        }

        private void FillLists()
        {
            if (CurrentUser != null)
            {
                _user = _userService.FindUserById(CurrentUser.Id);
                if (_user.Groups == null || _user.Groups.Count == 0)
                {
                    EnabledGroups = Groups;
                    DisabledGroups = new List<LightGroup>();
                }
                else
                {
                    List<LightGroup> enabled = new List<LightGroup>();
                    List<LightGroup> disabled = new List<LightGroup>();
                    foreach (var lightGroup in Groups)
                    {
                        if (_user.Groups.Select(g => g.Id).Contains(lightGroup.Id))
                        {
                            disabled.Add(lightGroup);
                        }
                        else
                        {
                            enabled.Add(lightGroup);
                        }
                    }

                    DisabledGroups = disabled;
                    EnabledGroups = enabled;
                }
            }
        }
    }
}