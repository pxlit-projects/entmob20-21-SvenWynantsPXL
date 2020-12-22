using SmartHouseLights.Models;
using SmartHouseLights.Services;
using SmartHouseLights.Services.Interfaces;

namespace SmartHouseLights.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public User User { get; set; }
        public HomeViewModel(IAuthenticationService authenticationService)
        {
            Title = "Home";
            User = authenticationService.GetUser();
        }
    }
}