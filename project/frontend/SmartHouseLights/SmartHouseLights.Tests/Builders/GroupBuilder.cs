using System;
using System.Collections.Generic;
using SmartHouseLights.Domain.Models;

namespace SmartHouseLights.Tests.Builders
{
    public class GroupBuilder
    {
        private LightGroup _group;

        public GroupBuilder()
        {
            _group = new LightGroup {AllOnState = false, HasOnState = false, Name = Guid.NewGuid().ToString()};
        }

        public GroupBuilder WithId(int id)
        {
            _group.Id = id;
            return this;
        }

        public GroupBuilder WithLights()
        {
            List<Light> lights = new List<Light>
            {
                new LightBuilder().WithId(1).WithDummy().Build(),
                new LightBuilder().WithId(2).WithDummy().Build()
            };

            _group.Lights = lights;
            return this;
        }

        public LightGroup Build()
        {
            return _group;
        }
    }
}