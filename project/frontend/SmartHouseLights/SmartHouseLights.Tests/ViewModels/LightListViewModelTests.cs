using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SmartHouseLights.Models;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Tests.Builders;
using SmartHouseLights.ViewModels;

namespace SmartHouseLights.Tests.ViewModels
{
    [TestFixture]
    public class LightListViewModelTests
    {
        private LightListViewModel _lightListViewModel;
        private Mock<INavigationService> _navServiceMock;
        private Mock<ILightService> _lightServiceMock;

        [SetUp]
        public void Setup()
        {
            _lightServiceMock.Setup(l => l.GetAllLights()).Returns(() =>
            {
                List<Light> lights = new List<Light>();
                lights.Add(new LightBuilder().WithDummy().Build());
                lights.Add(new LightBuilder().WithDummy().Build());
                return lights;
            });
            _lightListViewModel = new LightListViewModel(_navServiceMock.Object, _lightServiceMock.Object);
        }

        [Test]
        public void CreateModelShouldSetTitleAndLights()
        {
            Assert.That(_lightListViewModel.Title, Is.EqualTo("Lights"));
            Assert.That(_lightListViewModel.Lights, Is.Not.Null);
            Assert.That(_lightListViewModel.Lights.Count, Is.EqualTo(2));
        }


    }
}