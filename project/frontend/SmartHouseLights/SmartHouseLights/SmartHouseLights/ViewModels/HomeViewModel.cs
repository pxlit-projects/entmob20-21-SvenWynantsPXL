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
        public Command GoToStatisticsCommand => new Command(OnGoToStats);
        
        public HomeViewModel(INavigationService navService)
        {
            _navService = navService;
            Title = "Welcome";
        }

        private void OnGoToStats()
        {
            _navService.NavigateToAsync($"//{nameof(StatisticsView)}");
        }
    }
}