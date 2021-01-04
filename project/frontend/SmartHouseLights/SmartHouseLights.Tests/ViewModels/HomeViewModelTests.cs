using Moq;
using NUnit.Framework;
using SmartHouseLights.Models;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Tests.Builders;
using SmartHouseLights.ViewModels;

namespace SmartHouseLights.Tests.ViewModels
{
    [TestFixture]
    public class HomeViewModelTests
    {
        private HomeViewModel _homeViewModel;
        private Mock<IAuthenticationService> _authServiceMock;

        [SetUp]
        public void Setup()
        {
            _authServiceMock = new Mock<IAuthenticationService>();
        }

        [Test]
        public void CreatingHomeViewModelShouldSetCurrentUserInTitle()
        {
            User user = new UserBuilder().WithAdminUser().Build();
            _authServiceMock.Setup(a => a.GetUser())
                .Returns(() => user);
            _homeViewModel = new HomeViewModel(_authServiceMock.Object);

            Assert.That(_homeViewModel.User, Is.EqualTo(user));
            Assert.That(_homeViewModel.Title, Is.EqualTo("Welcome " + user.Name));
        }
    }
}