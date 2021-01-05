using Moq;
using NUnit.Framework;
using SmartHouseLights.Services.Interfaces;
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
    }
}