using Moq;
using NUnit.Framework;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Tests.Builders;
using SmartHouseLights.ViewModels;

namespace SmartHouseLights.Tests.ViewModels
{
    [TestFixture]
    public class LightDetailsViewModelTests
    {
        private Mock<ILightService> _lightServiceMock;
        private LightDetailsViewModel _model;

        [SetUp]
        public void Setup()
        {
            _lightServiceMock = new Mock<ILightService>();
            _model = new LightDetailsViewModel(_lightServiceMock.Object);
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
    }
}