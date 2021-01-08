using SmartHouseLights.Views;
using System.Windows.Input;
using SmartHouseLights.Services;
using SmartHouseLights.Services.Interfaces;
using Xamarin.Forms;

namespace SmartHouseLights.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authService;
        private readonly INavigationService _navigationService;
        public ICommand LoginCommand => new Command(OnLogin);

        private string _username;
        private string _password;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string ErrorMessage { get; set; }

        public LoginViewModel(IAuthenticationService authService, INavigationService navigationService)
        {
            _authService = authService;
            _navigationService = navigationService;
        }

        private async void OnLogin()
        {
            var result = await _authService.Login(Username, Password);
            if (result != null)
            {
                ErrorMessage = "";
                await _navigationService.NavigateToAsync($"//{nameof(HomeView)}");
            }
            else
            {
                ErrorMessage = "Incorrect password or username";
            }
        }
    }
}
