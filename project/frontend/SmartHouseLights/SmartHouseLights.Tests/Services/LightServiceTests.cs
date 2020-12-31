using System.Collections.Generic;
using Moq;
using NUnit.Framework;
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

        [SetUp]
        public void Setup()
        {
            _connFactoryMock = new Mock<IConnectionFactory>();
            _connFactoryMock.Setup(c => c.GetHttpClient()).Returns(() => new ClientBuilder().Build());
            _lightService = new LightService(_connFactoryMock.Object);
        }

        [Test]
        public void GetAllLightsShouldReturnListWithLightsFromDatabase()
        {
            List<Light> lights = _lightService.GetAllLights();

            Assert.That(lights, Is.Not.Null);
            Assert.That(lights.Count, Is.GreaterThan(2));
        }

        [Test]
        public void FlipSwitchShouldReturnLightWithChangedOnState()
        {
            List<Light> lights = _lightService.GetAllLights();
            Light unChangedLight = lights[0];

            Light returnedLight = _lightService.FlipSwitch(unChangedLight.Id);

            Assert.That(unChangedLight.OnState, Is.Not.EqualTo(returnedLight.OnState));
            
            Assert.That(unChangedLight.Id, Is.EqualTo(returnedLight.Id));

            returnedLight = _lightService.FlipSwitch(returnedLight.Id);

            Assert.That(unChangedLight.OnState, Is.EqualTo(returnedLight.OnState));
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
    }
}