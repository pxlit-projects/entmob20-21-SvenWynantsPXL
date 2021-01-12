using System;
using System.Collections.Generic;
using System.Linq;
using SmartHouseLights.Domain.Models;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Util;
using SmartHouseLights.Views;
using Xamarin.Forms;

namespace SmartHouseLights.ViewModels
{
    public class GroupListViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IGroupService _groupService;
        private readonly IAuthenticationService _authService;

        private User _user;
        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged();
                FlipSwitchCommand.ChangeCanExecute();
            }
        }

        public Command FlipSwitchCommand => new Command<int>(OnFlipPressed, OnCanFlipSwitch);
        public Command GroupSelectedCommand => new Command<int>(OnGroupSelected);

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }

        public Command RefreshListCommand => new Command(OnRefreshList);

        public Command AddGroupCommand => new Command(OnAddGroup);

        private List<LightGroup> _groups;
        public List<LightGroup> Groups
        {
            get => _groups;
            set
            {
                _groups = value;
                OnPropertyChanged();
            }
        }

        public GroupListViewModel(INavigationService navigationService, IGroupService groupService, IAuthenticationService authService)
        {
            _navigationService = navigationService;
            _groupService = groupService;
            _authService = authService;
            _user = authService.GetUser();
            Title = "LightGroups";
            Groups = _groupService.GetAllGroups();
            FlipSwitchCommand.ChangeCanExecute();
        }

        private void OnGroupSelected(int listId)
        {
            int id = Groups[listId].Id;
            LightGroup group = _groupService.GetGroupById(id);
            _navigationService.NavigateToAsync(nameof(GroupDetailView));
            MessagingCenter.Instance.Send(this, MessageConstants.GroupSelected, group);
        }

        private void OnFlipPressed(int id)
        {
            int listId = GetListId(id);
            if (Groups[listId].AllOnState)
            {
                _groupService.TurnAllLightsOffInGroup(id);
                Groups[listId].HasOnState = false;
            }
            else
            {
                _groupService.TurnAllLightsOnInGroup(id);
                Groups[listId].HasOnState = true;
            }

            Groups[listId].AllOnState = !Groups[GetListId(id)].AllOnState;
        }

        private void OnAddGroup()
        {
            _navigationService.NavigateToAsync(nameof(AddGroupView));
        }

        private void OnRefreshList()
        {
            IsRefreshing = true;
            User = _authService.GetUser();
            Groups = _groupService.GetAllGroups();
            IsRefreshing = false;
        }

        private bool OnCanFlipSwitch(int id)
        {
            if (Groups[GetListId(id)].Lights?.Count > 0)
            {
                if (User.Groups == null || User.Groups.Count < 1)
                {
                    return true;
                }

                return _user.Groups.All(lightGroup => id != lightGroup.Id);
            }
            return false;
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
            throw new InvalidOperationException(id.ToString());
        }
    }
}