using System.Collections.Generic;
using SmartHouseLights.Data.Services.Interfaces;
using SmartHouseLights.Domain.Models;
using SmartHouseLights.Services.Interfaces;

namespace SmartHouseLights.ViewModels
{
    public class StatisticsViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authService;
        private readonly IStatisticsService _statisticsService;
        private User _user;
        public List<UserLightStatistic> Statistics { get; set; }
        public StatisticsViewModel(IAuthenticationService authService, IStatisticsService statisticsService)
        {
            _authService = authService;
            _statisticsService = statisticsService;
            _user = authService.GetUser();

            Title = $"Statistics for {_user.Name}";

            Statistics = _statisticsService.GetAllStatisticsForUserWithId(_user.Id);
        }
    }
}