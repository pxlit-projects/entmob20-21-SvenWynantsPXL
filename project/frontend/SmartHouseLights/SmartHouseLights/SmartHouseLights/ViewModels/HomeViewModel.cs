using System;
using System.Windows.Input;
using SmartHouseLights.Models;
using SmartHouseLights.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

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