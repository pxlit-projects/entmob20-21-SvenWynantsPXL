using System;
using SmartHouseLights.Models;

namespace SmartHouseLights.Tests.Builders
{
    public class LightBuilder
    {
        private readonly Light _light;
        private static int _id = 1;
        public LightBuilder()
        {
            _light = new Light();
            _light.Id = _id;
            _id += 1;
            _light.Name = Guid.NewGuid().ToString();
        }

        public LightBuilder WithDummy()
        {
            _light.Manufacturer = Manufacturer.DUMMY;
            return this;
        }

        public Light Build()
        {
            return _light;
        }
    }
}