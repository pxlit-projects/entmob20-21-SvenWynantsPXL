using System.Collections.Generic;
using SmartHouseLights.Data.Services.Interfaces;
using SmartHouseLights.Domain.Models;
using SmartHouseLights.Services.Interfaces;

namespace SmartHouseLights.ViewModels
{
    public class StatisticsViewModel : ViewModelBase
    {
        public List<UserLightStatistic> Statistics { get; set; }

        public StatisticsViewModel(IAuthenticationService authService, IStatisticsService statisticsService)
        {
            var user = authService.GetUser();

            Title = $"Statistics for {user.Name}";

            Statistics = statisticsService.GetAllStatisticsForUserWithId(user.Id);
        }
    }
}