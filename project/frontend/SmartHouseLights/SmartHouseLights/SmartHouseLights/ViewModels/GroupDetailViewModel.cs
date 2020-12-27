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

        public GroupDetailViewModel(IGroupService groupService)
        {
            _groupService = groupService;
            MessagingCenter.Instance.Subscribe<GroupListViewModel, LightGroup>(this, MessageConstants.GroupSelected,
                (sender, group) => { Group = group; });
            Title = $"Group: {Group.Name}";
        }
    }
}