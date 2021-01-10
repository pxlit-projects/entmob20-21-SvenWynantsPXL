using System.Collections.Generic;
using SmartHouseLights.Data.Services.Interfaces;
using SmartHouseLights.Domain.Models;
using SmartHouseLights.Services.Interfaces;
using Xamarin.Forms;

namespace SmartHouseLights.ViewModels
{
    public class StatisticsViewModel : ViewModelBase
    {
        private readonly IStatisticsService _statisticsService;
        private readonly User _user;

        private List<UserLightStatistic> _statistics;
        public List<UserLightStatistic> Statistics
        {
            get => _statistics;
            set
            {
                _statistics = value;
                OnPropertyChanged();
            }
        }

        public Command RefreshStatsCommand => new Command(OnRefreshStats);

        public StatisticsViewModel(IAuthenticationService authService, IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
            _user = authService.GetUser();

            Title = $"Statistics for {_user.Name}";
            
            Statistics = statisticsService.GetAllStatisticsForUserWithId(_user.Id);
        }

        private void OnRefreshStats()
        {
            Statistics = _statisticsService.GetAllStatisticsForUserWithId(_user.Id);
        }
    }
}