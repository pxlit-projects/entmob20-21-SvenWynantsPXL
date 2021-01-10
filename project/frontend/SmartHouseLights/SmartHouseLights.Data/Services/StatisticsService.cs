using System;
using System.Collections.Generic;
using System.Linq;
using SmartHouseLights.Data.Services.Interfaces;
using SmartHouseLights.Domain.Models;

namespace SmartHouseLights.Data.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly SmartHouseContext _context;

        public StatisticsService(SmartHouseContext context)
        {
            _context = context;
        }

        public UserLightStatistic GetStatisticByUserIdAndLightId(int userId, int lightId)
        {
            try
            {
                UserLightStatistic statistic = _context.UserLightStatistics.First(u => u.UserId == userId && u.LightId == lightId);
                return statistic;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void SaveStatistic(UserLightStatistic statistic)
        {
            _context.UserLightStatistics.Update(statistic);
        }

        public void AddStatistic(UserLightStatistic statistic)
        {
            _context.UserLightStatistics.Add(statistic);
        }

        public List<UserLightStatistic> GetAllStatisticsForUserWithId(int userId)
        {
            return _context.UserLightStatistics.Where(u => u.UserId == userId).ToList();
        }
    }
}