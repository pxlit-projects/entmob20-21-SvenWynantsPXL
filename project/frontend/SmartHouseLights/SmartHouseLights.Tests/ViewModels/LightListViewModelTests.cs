using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SmartHouseLights.Models;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Tests.Builders;
using SmartHouseLights.Util;
using SmartHouseLights.ViewModels;
using Xamarin.Forms;

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
            _navServiceMock = new Mock<INavigationService>();
            _lightServiceMock = new Mock<ILightService>();
            List<Light> lights = new List<Light>();
            lights.Add(new LightBuilder().WithDummy().WithId(1).Build());
            lights.Add(new LightBuilder().WithDummy().WithId(2).Build());

            _lightServiceMock.Setup(l => l.GetAllLights()).Returns(() => lights);
            _lightListViewModel = new LightListViewModel(_navServiceMock.Object, _lightServiceMock.Object);
        }

        [Test]
        public void CreateModelShouldSetTitleAndLights()
        {
            Assert.That(_lightListViewModel.Title, Is.EqualTo("Lights"));
            Assert.That(_lightListViewModel.Lights, Is.Not.Null);
            Assert.That(_lightListViewModel.Lights.Count, Is.EqualTo(2));
        }

        [Test]
        public void OnFlipSwitchShouldReturnLightWithFlippedSwitch()
        {
            Light light = _lightListViewModel.Lights[0];

            light.OnState = false;

            _lightListViewModel.FlipSwitchCommand.Execute(1);

            Assert.That(light.OnState, Is.True);
        }
    }
}