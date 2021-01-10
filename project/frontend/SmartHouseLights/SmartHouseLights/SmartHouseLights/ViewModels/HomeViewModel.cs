using SmartHouseLights.Domain.Models;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Util;
using SmartHouseLights.Views;
using Xamarin.Forms;

namespace SmartHouseLights.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly INavigationService _navService;
        private readonly IAuthenticationService _authService;
        public Command GoToStatisticsCommand => new Command(OnGoToStats);

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
        public Command GoToUserManagementCommand => new Command(OnGoToUserManagement, OnAdmin);
        public HomeViewModel(INavigationService navService, IAuthenticationService authService)
        {
            _navService = navService;
            _authService = authService;
            User = authService.GetUser();
            Title = "Welcome " + authService.GetUser().Name;
        }

        private void OnGoToStats()
        {
            _navService.NavigateToAsync($"//{nameof(StatisticsView)}");
        }

        private void OnGoToUserManagement()
        {
            _navService.NavigateToAsync(nameof(UserManagementView));
        }

        private bool OnAdmin()
        {
            if (_authService.GetUser().Role.Equals("ROLE_ADMIN"))
            {
                return true;
            }

            return false;
        }
    }
}