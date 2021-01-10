using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SmartHouseLights.Domain.Models;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Tests.Builders;
using SmartHouseLights.ViewModels;
using SmartHouseLights.Views;

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
            List<Light> lights = new List<Light>
            {
                new LightBuilder().WithDummy().WithId(1).Build(),
                new LightBuilder().WithDummy().WithId(2).Build()
            };

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

            _lightServiceMock.Verify(l => l.FlipSwitch(1), Times.Once);

            Assert.That(light.OnState, Is.True);
        }

        [Test]
        public void RefreshListCommandShouldRefreshExistingList()
        {
            _lightServiceMock.Setup(l => l.GetAllLights()).Returns(() => new List<Light>());

            Assert.That(_lightListViewModel.IsRefreshing, Is.False);
            _lightListViewModel.IsRefreshing = true;
            _lightListViewModel.RefreshListCommand.Execute(null);

            Assert.That(_lightListViewModel.IsRefreshing, Is.False);
            Assert.That(_lightListViewModel.Lights, Is.Empty);
        }

        [Test]
        public void OnLightSelectedShouldNavigateToDetailPage()
        {
            int id = 0;
            Light light = _lightListViewModel.Lights[id];
            _lightServiceMock.Setup(l => l.GetLightById(1)).Returns(() => light);

            _lightListViewModel.LightSelectedCommand.Execute(id);

            _lightServiceMock.Verify(l => l.GetLightById(1), Times.Once);
            _navServiceMock.Verify(n => n.NavigateToAsync(nameof(LightDetailsView)), Times.Once);
        }

        [Test]
        public void OnClickAddShouldNavigateToAddLightView()
        {
            _lightListViewModel.AddLightCommand.Execute(null);

            _navServiceMock.Verify(n => n.NavigateToAsync(nameof(AddLightView)), Times.Once);
        }
    }
}