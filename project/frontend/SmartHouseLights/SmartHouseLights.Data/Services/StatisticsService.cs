using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
            _context.SaveChanges();
        }

        public void AddStatistic(UserLightStatistic statistic)
        {
            if (_context.Lights.Find(statistic.LightId) != null)
            {
                statistic.Light = _context.Lights.Find(statistic.LightId);
            }
            _context.UserLightStatistics.Add(statistic);
            _context.SaveChanges();
        }

        public List<UserLightStatistic> GetAllStatisticsForUserWithId(int userId)
        {
            return _context.UserLightStatistics.Where(u => u.UserId == userId).Include(s => s.Light).ToList();
        }
    }
}