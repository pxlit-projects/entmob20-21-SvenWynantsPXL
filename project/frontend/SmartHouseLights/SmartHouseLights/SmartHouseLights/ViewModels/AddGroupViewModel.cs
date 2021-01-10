using SmartHouseLights.Domain.Models;
using SmartHouseLights.Models;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Views;
using Xamarin.Forms;

namespace SmartHouseLights.ViewModels
{
    public class AddGroupViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IGroupService _groupService;
        public Command SaveGroupCommand => new Command(OnSaveGroup);
        
        private CreateGroupModel _groupModel;
        public CreateGroupModel GroupModel
        {
            get => _groupModel;
            set
            {
                _groupModel = value;
                OnPropertyChanged();
            }
        }

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

        public AddGroupViewModel(INavigationService navigationService, IGroupService groupService)
        {
            _navigationService = navigationService;
            _groupService = groupService;
            Title = "Add a group";
            GroupModel = new CreateGroupModel();
        }

        private void OnSaveGroup()
        {
            if (GroupModel.Name != null && !GroupModel.Name.Equals(""))
            {
                LightGroup group = _groupService.AddGroup(GroupModel);
                if (group == null)
                {
                    ErrorMessage = "Something went wrong adding the group";
                }
                else
                {
                    ErrorMessage = "";
                    _navigationService.NavigateToAsync($"//{nameof(GroupListView)}");
                }
            }
            else
            {
                ErrorMessage = "You need to fill in Name";
            }
        }
    }
}