using SmartHouseLights.Domain.Models;
using SmartHouseLights.Models;

namespace SmartHouseLights.Tests.Builders
{
    public class CreateLightBuilder
    {
        private readonly CreateLightModel _light;

        public CreateLightBuilder()
        {
            _light = new CreateLightModel();
        }

        public CreateLightBuilder WithName()
        {
            _light.Name = "Test Light";
            return this;
        }

        public CreateLightBuilder WithDummyManufacturer()
        {
            _light.LightManufacturer = Manufacturer.DUMMY;
            return this;
        }

        public CreateLightBuilder WithType()
        {
            _light.Type = "Test light";
            return this;
        }

        public CreateLightModel Build()
        {
            return _light;
        }
    }
}