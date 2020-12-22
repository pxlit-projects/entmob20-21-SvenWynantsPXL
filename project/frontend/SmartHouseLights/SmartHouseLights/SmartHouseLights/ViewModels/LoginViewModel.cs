using SmartHouseLights.Views;
using System.Windows.Input;
using SmartHouseLights.Services;
using Xamarin.Forms;

namespace SmartHouseLights.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
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

        private async void OnLogin()
        {
            AuthenticationService service = new AuthenticationService();
            var result = await service.Login(Username, Password);
            if (result != null)
            {
                await Shell.Current.GoToAsync($"//{nameof(HomeView)}");
            }
        }
    }
}
