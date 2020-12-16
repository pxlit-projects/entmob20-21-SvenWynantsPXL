using SmartHouseLights.Models;
using SmartHouseLights.Services;

namespace SmartHouseLights.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public User User { get; set; }
        public HomeViewModel()
        {
            Title = "Home";
            User = AuthenticationService._user;
        }
    }
}