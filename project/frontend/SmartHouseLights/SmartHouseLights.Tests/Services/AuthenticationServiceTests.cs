using Moq;
using NUnit.Framework;
using SmartHouseLights.Domain.Models;
using SmartHouseLights.Services;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Tests.Builders;

namespace SmartHouseLights.Tests.Services
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        private IAuthenticationService _authService;
        private Mock<IConnectionFactory> _connFactoryMock;

        [SetUp]
        public void Setup()
        {
            _connFactoryMock = new Mock<IConnectionFactory>();
            _connFactoryMock.Setup(c => c.GetHttpClient())
                .Returns(() => new ClientBuilder().Build());

            _authService = new AuthenticationService(_connFactoryMock.Object);
        }

        [Test]
        public void LoginShouldReturnNullIfNameOrPasswordIsIncorrect()
        {
            User user = _authService.Login("sven", "incorrect").Result;

            Assert.That(user, Is.Null);

            user = _authService.Login("incorrect", "pxl").Result;

            Assert.That(user, Is.Null);
        }

        [Test]
        public void LoginShouldReturnUserIfNameAndPasswordAreCorrect()
        {
            User user = _authService.Login("sven", "pxl").Result;

            Assert.That(user, Is.Not.Null);
            Assert.That(user.Name, Is.EqualTo("sven"));
        }

        [Test]
        public void GetUserShouldReturnUserAfterLogin()
        {
            Assert.That(_authService.GetUser(), Is.Null);

            User user = _authService.Login("sven", "pxl").Result;

            User getUser = _authService.GetUser();

            Assert.That(getUser, Is.Not.Null);
            Assert.That(user.Name, Is.EqualTo(getUser.Name));
        }
    }
}