using System.Collections.Generic;
using SmartHouseLights.Domain.Models;

namespace SmartHouseLights.Data.Services.Interfaces
{
    public interface IStatisticsService
    {
        UserLightStatistic GetStatisticByUserIdAndLightId(int userId, int lightId);
        void SaveStatistic(UserLightStatistic statistic);
        void AddStatistic(UserLightStatistic statistic);
        List<UserLightStatistic> GetAllStatisticsForUserWithId(int userId);
        void DeleteStatisticsByLight(int lightId);
    }
}