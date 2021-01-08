using System;
using SmartHouseLights.Domain.Models;

namespace SmartHouseLights.Tests.Builders
{
    public class LightBuilder
    {
        private readonly Light _light;

        public LightBuilder()
        {
            _light = new Light
            {
                OnState = false,
                Name = Guid.NewGuid().ToString()
            };
        }

        public LightBuilder WithDummy()
        {
            _light.Manufacturer = Manufacturer.DUMMY;
            return this;
        }

        public LightBuilder WithId(int id)
        {
            _light.Id = id;
            return this;
        }

        public Light Build()
        {
            return _light;
        }
    }
}