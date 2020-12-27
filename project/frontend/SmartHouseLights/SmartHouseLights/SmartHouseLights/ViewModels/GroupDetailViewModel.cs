using SmartHouseLights.Models;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Util;
using Xamarin.Forms;

namespace SmartHouseLights.ViewModels
{
    public class GroupDetailViewModel : ViewModelBase
    {
        private readonly IGroupService _groupService;

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

        private int _groupId;
        public GroupDetailViewModel(IGroupService groupService)
        {
            _groupService = groupService;
            MessagingCenter.Instance.Subscribe<GroupListViewModel, int>(this, MessageConstants.GroupSelected,
                (sender, groupId) =>
                {
                    _groupId = groupId; 
                    Group = groupService.GetGroupById(_groupId);
                });
            Title = $"Group: {Group.Name}";
        }
    }
}