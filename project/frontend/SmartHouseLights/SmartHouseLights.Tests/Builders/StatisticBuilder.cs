using System;
using SmartHouseLights.Domain.Models;

namespace SmartHouseLights.Tests.Builders
{
    public class StatisticBuilder
    {
        private UserLightStatistic _stat;
        public StatisticBuilder()
        {
            _stat = new UserLightStatistic
            {
                TurnedOnTime = DateTime.Now,
                UserId = 1,
                HoursOn = 0
            };
        }

        public StatisticBuilder WithLight(int id)
        {
            _stat.Light = new LightBuilder().WithId(id).Build();
            _stat.LightId = id;
            return this;
        }

        public StatisticBuilder WithUserId(int id)
        {
            _stat.UserId = id;
            return this;
        }

        public StatisticBuilder WithLight(Light light)
        {
            _stat.Light = light;
            _stat.LightId = light.Id;

            return this;
        }

        public UserLightStatistic Build()
        {
            return _stat;
        }
    }
}