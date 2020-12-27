using System.Collections.Generic;
using SmartHouseLights.Models;
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

        private Command _flipSwitchCommand;

        public Command FlipSwitchCommand => _flipSwitchCommand ??
                                            (_flipSwitchCommand = new Command<int>(OnFlipPressed));
        public Command GroupSelectedCommand => new Command<int>(OnGroupSelected);

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

        public GroupListViewModel(INavigationService navigationService, IGroupService groupService)
        {
            _navigationService = navigationService;
            _groupService = groupService;
            Title = "LightGroups";
            Groups = _groupService.GetAllGroups();
        }

        private void OnGroupSelected(int id)
        { 
            _navigationService.NavigateToAsync(nameof(GroupDetailView));
            MessagingCenter.Instance.Send(this, MessageConstants.GroupSelected, id);
        }

        private void OnFlipPressed(int id)
        {
            if (Groups[GetListId(id)].AllOnState)
            {
                _groupService.TurnAllLightsOffInGroup(id);
            }
            else
            {
                _groupService.TurnAllLightsOnInGroup(id);
            }

            Groups[GetListId(id)].AllOnState = !Groups[GetListId(id)].AllOnState;
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