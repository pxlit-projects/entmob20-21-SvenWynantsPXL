using System;
using SmartHouseLights.Domain.Models;

namespace SmartHouseLights.Tests.Builders
{
    public class UserLightStatisticBuilder
    {
        private UserLightStatistic _stat;
        public UserLightStatisticBuilder()
        {
            _stat = new UserLightStatistic
            {
                TurnedOnTime = DateTime.Now,
                UserId = 1,
                HoursOn = 0
            };
        }

        public UserLightStatisticBuilder WithLight(int id)
        {
            _stat.Light = new LightBuilder().WithId(id).Build();
            _stat.LightId = id;
            return this;
        }

        public UserLightStatistic Build()
        {
            return _stat;
        }
    }
}