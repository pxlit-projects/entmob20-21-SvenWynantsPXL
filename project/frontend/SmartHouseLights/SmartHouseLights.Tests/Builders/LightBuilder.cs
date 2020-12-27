using SmartHouseLights.Models;

namespace SmartHouseLights.Tests.Builders
{
    public class LightBuilder
    {
        private readonly CreateLightModel _light;

        public LightBuilder()
        {
            _light = new CreateLightModel();
        }

        public LightBuilder WithName()
        {
            _light.Name = "Test Light";
            return this;
        }

        public LightBuilder WithDummyManufacturer()
        {
            _light.LightManufacturer = Manufacturer.DUMMY;
            return this;
        }

        public LightBuilder WithType()
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