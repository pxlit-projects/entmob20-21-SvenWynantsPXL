using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SmartHouseLights.Data.Services.Interfaces;
using SmartHouseLights.Domain.Models;
using SmartHouseLights.Models;
using SmartHouseLights.Services;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Tests.Builders;

namespace SmartHouseLights.Tests.Services
{
    [TestFixture]
    public class LightServiceTests
    {
        private ILightService _lightService;
        private Mock<IConnectionFactory> _connFactoryMock;
        private Mock<IStatisticsService> _statisticServiceMock;
        private Mock<IAuthenticationService> _authServiceMock;

        [SetUp]
        public void Setup()
        {
            _statisticServiceMock = new Mock<IStatisticsService>();
            _connFactoryMock = new Mock<IConnectionFactory>();
            _authServiceMock = new Mock<IAuthenticationService>();

            _connFactoryMock.Setup(c => c.GetHttpClient()).Returns(() => new ClientBuilder().Build());
            _lightService = new LightService(_connFactoryMock.Object, _statisticServiceMock.Object, _authServiceMock.Object);
        }

        [Test]
        public void GetAllLightsShouldReturnListWithLightsFromDatabase()
        {
            List<Light> lights = _lightService.GetAllLights();

            Assert.That(lights, Is.Not.Null);
            Assert.That(lights.Count, Is.GreaterThan(2));
        }

        [Test]
        public void AddLightShouldAddLightToDatabase()
        {
            List<Light> lights = _lightService.GetAllLights();
            int initCount = lights.Count;

            CreateLightModel lightModel = new CreateLightBuilder().WithDummyManufacturer().WithName().WithType().Build();

            Light light = _lightService.AddLight(lightModel);

            lights = _lightService.GetAllLights();
            int newCount = lights.Count;

            Assert.That(newCount, Is.EqualTo(initCount + 1));
            Assert.That(lightModel.Name, Is.EqualTo(light.Name));
            Assert.That(lightModel.Type, Is.EqualTo(light.Type));
            Assert.That(light.Id, Is.Not.Null);
        }

        [Test]
        public void DeleteLightByIdShouldReturnFalseOnFail()
        {
            bool success = _lightService.DeleteLightById(0);

            Assert.False(success);
        }

        [Test]
        public void DeleteLightByIdShouldReturnTrueOnSuccess()
        {
            CreateLightModel model = new CreateLightBuilder().WithDummyManufacturer().WithName().WithType().Build();
            Light light = _lightService.AddLight(model);

            Assert.That(light, Is.Not.Null);

            bool success = _lightService.DeleteLightById(light.Id);

            Assert.True(success);
        }

        [Test]
        public void UpdateLightShouldUpdateExistingLight()
        {
            Light light = _lightService.GetLightById(1);
            light.Brightness = 50;

            Light updatedLight = _lightService.UpdateLight(light);

            Assert.That(light.Id, Is.EqualTo(updatedLight.Id));
            Assert.That(light.Brightness, Is.EqualTo(updatedLight.Brightness));
            Assert.That(updatedLight.Brightness, Is.EqualTo(50));

            updatedLight.Brightness = 100;
            updatedLight = _lightService.UpdateLight(updatedLight);

            Assert.That(light.Brightness, Is.Not.EqualTo(updatedLight.Brightness));
            Assert.That(updatedLight.Brightness, Is.EqualTo(100));
        }
    }
}