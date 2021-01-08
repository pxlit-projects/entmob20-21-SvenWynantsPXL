using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SmartHouseLights.Domain.Models;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Tests.Builders;
using SmartHouseLights.ViewModels;

namespace SmartHouseLights.Tests.ViewModels
{
    [TestFixture]
    public class LightDetailsViewModelTests
    {
        private Mock<ILightService> _lightServiceMock;
        private Mock<INavigationService> _navServiceMock;
        private Mock<IGroupService> _groupServiceMock;
        private LightDetailsViewModel _model;

        [SetUp]
        public void Setup()
        {
            _lightServiceMock = new Mock<ILightService>();
            _navServiceMock = new Mock<INavigationService>();
            _groupServiceMock = new Mock<IGroupService>();

            _groupServiceMock.Setup(g => g.GetAllGroups())
                .Returns(() =>
                {
                    List<LightGroup> groups = new List<LightGroup>
                    {
                        new GroupBuilder().WithId(1).Build(),
                        new GroupBuilder().WithId(2).Build()
                    };
                    return groups;
                });

            _model = new LightDetailsViewModel(_lightServiceMock.Object, _navServiceMock.Object, _groupServiceMock.Object);
        }

        [Test]
        public void OnFlipSwitchShouldTurnLightOnIfLightWasOff()
        {
            _model.Light = new LightBuilder().WithId(1).WithDummy().Build();
            _model.Light.OnState = false;

            _lightServiceMock.Setup(l => l.FlipSwitch(1)).Returns(() =>
            {
                _model.Light.OnState = !_model.Light.OnState;
                return _model.Light;
            });

            _model.FlipSwitchCommand.Execute(null);

            Assert.That(_model.Light.OnState, Is.True);
            _lightServiceMock.Verify(l => l.FlipSwitch(1), Times.Once);
        }

        [Test]
        public void OnFlipSwitchShouldTurnLightOffIfLightWasOn()
        {
            _model.Light = new LightBuilder().WithId(1).WithDummy().Build();
            _model.Light.OnState = true;

            _lightServiceMock.Setup(l => l.FlipSwitch(1)).Returns(() =>
            {
                _model.Light.OnState = !_model.Light.OnState;
                return _model.Light;
            });

            _model.FlipSwitchCommand.Execute(null);

            Assert.That(_model.Light.OnState, Is.False);
            _lightServiceMock.Verify(l => l.FlipSwitch(1), Times.Once);
        }

        [Test]
        public void OnFlipShouldRefreshCanExecute()
        {
            _model.Light = new LightBuilder().WithId(1).WithDummy().Build();
            _model.Light.OnState = false;

            _lightServiceMock.Setup(l => l.FlipSwitch(1)).Returns(() =>
            {
                _model.Light.OnState = !_model.Light.OnState;
                return _model.Light;
            });

            _model.FlipSwitchCommand.Execute(null);

            Assert.That(_model.OnDragCompletedCommand.CanExecute(null), Is.True);

            _model.FlipSwitchCommand.Execute(null);

            Assert.That(_model.OnDragCompletedCommand.CanExecute(null), Is.False);
        }

        [Test]
        public void OnDragCompletedShouldCallUpdateLight()
        {
            _model.Light = new LightBuilder().WithId(1).WithDummy().Build();

            _lightServiceMock.Setup(l => l.UpdateLight(_model.Light)).Returns(() => _model.Light);

            _model.OnDragCompletedCommand.Execute(null);

            _lightServiceMock.Verify(l => l.UpdateLight(_model.Light), Times.Once);
        }
    }
}