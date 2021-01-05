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

        public LoginViewModel(IAuthenticationService authService)
        {
            _authService = authService;
        }

        private async void OnLogin()
        {
            var result = await _authService.Login(Username, Password);
            if (result != null)
            {
                ErrorMessage = "";
                await Shell.Current.GoToAsync($"//{nameof(HomeView)}");
            }
            else
            {
                ErrorMessage = "Incorrect password or username";
            }
        }
    }
}
