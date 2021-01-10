using SmartHouseLights.Domain.Models;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Views;
using Xamarin.Forms;

namespace SmartHouseLights.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly INavigationService _navService;
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

        public HomeViewModel(IAuthenticationService authenticationService, INavigationService navService)
        {
            _navService = navService;
            User = authenticationService.GetUser();
            Title = "Welcome " + User.Name;
        }

        private void OnGoToStats()
        {
            _navService.NavigateToAsync($"//{nameof(StatisticsView)}");
        }
    }
}