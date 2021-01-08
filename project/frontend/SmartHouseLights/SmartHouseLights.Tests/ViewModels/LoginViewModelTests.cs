using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SmartHouseLights.Domain.Models;
using SmartHouseLights.Models;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Tests.Builders;
using SmartHouseLights.ViewModels;
using SmartHouseLights.Views;

namespace SmartHouseLights.Tests.ViewModels
{
    [TestFixture]
    public class LoginViewModelTests
    {
        private LoginViewModel _model;
        private Mock<IAuthenticationService> _authServiceMock;
        private Mock<INavigationService> _navServiceMock;

        [SetUp]
        public void Setup()
        {
            _authServiceMock = new Mock<IAuthenticationService>();
            _navServiceMock = new Mock<INavigationService>();
            _model = new LoginViewModel(_authServiceMock.Object, _navServiceMock.Object);
        }

        [Test]
        public void OnLoginShouldSetErrorMessageIfFailed()
        {
            _authServiceMock.Setup(a => a.Login("Sven", "pxl")).Returns(() => null);

            _model.LoginCommand.Execute(null);

            Assert.That(_model.ErrorMessage, Is.EqualTo("Incorrect password or username"));
            _navServiceMock.Verify(n => n.NavigateToAsync($"//{nameof(HomeView)}"), Times.Never);
        }

        [Test]
        public void OnLoginShouldClearErrorMessageOnSuccess()
        {
            _model.ErrorMessage = "Incorrect password or username";
            User user = new UserBuilder().WithAdminUser().Build();
            _authServiceMock.Setup(a => a.Login("sven", "pxl")).Returns(() => Task.FromResult(user));
            _model.Username = "sven";
            _model.Password = "pxl";

            _model.LoginCommand.Execute(null);

            Assert.That(_model.ErrorMessage, Is.EqualTo(""));
            _navServiceMock.Verify(n => n.NavigateToAsync($"//{nameof(HomeView)}"), Times.Once);
        }
    }
}