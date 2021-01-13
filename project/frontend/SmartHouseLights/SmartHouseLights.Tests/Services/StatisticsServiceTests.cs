using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SmartHouseLights.Data;
using SmartHouseLights.Data.Services;
using SmartHouseLights.Domain.Models;
using SmartHouseLights.Tests.Builders;

namespace SmartHouseLights.Tests.Services
{
    [TestFixture]
    public class StatisticsServiceTests
    {
        private StatisticsService _statisticsService;
        private SmartHouseContext _context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<SmartHouseContext>()
                .UseInMemoryDatabase(databaseName: "SmartHomeDatabase")
                .Options;
            _context = new SmartHouseContext(options);
            FillContextWithStatistics();

            _statisticsService = new StatisticsService(_context);
        }

        [Test]
        public void AddStatisticShouldAddStatistic()
        {
            UserLightStatistic statistic = new UserLightStatistic();
            statistic.Light = new LightBuilder().WithId(1).Build();
            statistic.TurnedOnTime = DateTime.Now;
            statistic.HoursOn = 2;
            statistic.UserId = 2;
            statistic.LightId = 1;

            _statisticsService.AddStatistic(statistic);

            Assert.That(_context.UserLightStatistics.ToList().Count, Is.EqualTo(3));
        }

        private void FillContextWithStatistics()
        {
            Light light = new LightBuilder().WithId(1).WithDummy().Fill().Build();
            Light light2 = new LightBuilder().WithId(2).WithDummy().Fill().Build();
            _context.Lights.Add(light);
            _context.Lights.Add(light2);
            
            UserLightStatistic stat1 = new StatisticBuilder().WithLight(light).WithUserId(1).Build();
            UserLightStatistic stat2 = new StatisticBuilder().WithLight(light2).WithUserId(1).Build();
            _context.UserLightStatistics.Add(stat1);
            _context.UserLightStatistics.Add(stat2);
            _context.SaveChanges();
        }
    }
}