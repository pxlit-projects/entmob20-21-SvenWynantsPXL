using System.Collections.Generic;
using SmartHouseLights.Data.Services.Interfaces;
using SmartHouseLights.Domain.Models;
using SmartHouseLights.Services.Interfaces;
using Xamarin.Forms;

namespace SmartHouseLights.ViewModels
{
    public class StatisticsViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authService;
        private readonly IStatisticsService _statisticsService;

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

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }

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
            _authService = authService;
            _statisticsService = statisticsService;
            _user = authService.GetUser();

            Title = $"Statistics for {User.Name}";
            
            Statistics = statisticsService.GetAllStatisticsForUserWithId(User.Id);
        }

        private void OnRefreshStats()
        {
            IsRefreshing = true;
            User = _authService.GetUser();
            Title = $"Statistics for {User.Name}";
            Statistics = _statisticsService.GetAllStatisticsForUserWithId(User.Id);
            IsRefreshing = false;
        }
    }
}