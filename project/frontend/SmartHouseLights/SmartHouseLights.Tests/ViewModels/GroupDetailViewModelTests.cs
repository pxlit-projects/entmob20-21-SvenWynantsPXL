﻿using Moq;
using NUnit.Framework;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Tests.Builders;
using SmartHouseLights.ViewModels;

namespace SmartHouseLights.Tests.ViewModels
{
    [TestFixture]
    public class GroupDetailViewModelTests
    {
        private GroupDetailViewModel _model;
        private Mock<IGroupService> _groupServiceMock;

        [SetUp]
        public void Setup()
        {
            _groupServiceMock = new Mock<IGroupService>();
            _model = new GroupDetailViewModel(_groupServiceMock.Object);
        }

        [Test]
        public void OnFlipSwitchShouldReturnGroupWithAllLightsOnIfAllOnStateWasOff()
        {
            _model.Group = new GroupBuilder().WithId(1).WithLights().Build();
            _groupServiceMock.Setup(g => g.TurnAllLightsOnInGroup(1))
                .Returns(() => _model.Group);
            
            _model.FlipSwitchCommand.Execute(null);

            _groupServiceMock.Verify(g => g.TurnAllLightsOnInGroup(1), Times.Once);
            _groupServiceMock.Verify(g => g.TurnAllLightsOffInGroup(1), Times.Never);
            Assert.That(_model.Group.AllOnState, Is.True);
            Assert.That(_model.Group.HasOnState, Is.True);

            foreach (var light in _model.Group.Lights)
            {
                Assert.That(light.OnState, Is.True);
            }
        }

        [Test]
        public void OnFlipSwitchShouldReturnGroupWithAllLightsOffIfAllOnStateWasOn()
        {
            _model.Group = new GroupBuilder().WithId(1).WithLights().Build();
            _model.Group.AllOnState = true;
            _model.Group.HasOnState = true;
            foreach (var light in _model.Group.Lights)
            {
                light.OnState = true;
            }

            _groupServiceMock.Setup(g => g.TurnAllLightsOffInGroup(1))
                .Returns(() => _model.Group);

            _model.FlipSwitchCommand.Execute(null);

            _groupServiceMock.Verify(g => g.TurnAllLightsOffInGroup(1), Times.Once);
            _groupServiceMock.Verify(g => g.TurnAllLightsOnInGroup(1), Times.Never);

            Assert.That(_model.Group.AllOnState, Is.False);
            Assert.That(_model.Group.HasOnState, Is.False);
            foreach (var light in _model.Group.Lights)
            {
                Assert.That(light.OnState, Is.False);
            }
        }
    }
}