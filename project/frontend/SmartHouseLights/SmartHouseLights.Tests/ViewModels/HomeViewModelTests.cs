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
    public class HomeViewModelTests
    {
        private HomeViewModel _homeViewModel;
        private Mock<IAuthenticationService> _authServiceMock;
        private Mock<INavigationService> _navServiceMock;

        [SetUp]
        public void Setup()
        {
            _navServiceMock = new Mock<INavigationService>();
            _authServiceMock = new Mock<IAuthenticationService>();
        }

        [Test]
        public void CreatingHomeViewModelShouldSetCurrentUserInTitle()
        {
            User user = new UserBuilder().WithAdminUser().Build();
            _authServiceMock.Setup(a => a.GetUser())
                .Returns(() => user);
            _homeViewModel = new HomeViewModel(_authServiceMock.Object, _navServiceMock.Object);

            Assert.That(_homeViewModel.User, Is.EqualTo(user));
            Assert.That(_homeViewModel.Title, Is.EqualTo("Welcome " + user.Name));
        }

        [Test]
        public void GoToStatisticPageShouldNavigate()
        {
            User user = new UserBuilder().WithAdminUser().Build();
            _authServiceMock.Setup(a => a.GetUser())
                .Returns(() => user);
            _homeViewModel = new HomeViewModel(_authServiceMock.Object, _navServiceMock.Object);
            _homeViewModel.GoToStatisticsCommand.Execute(null);

            _navServiceMock.Verify(n => n.NavigateToAsync($"//{nameof(StatisticsView)}"), Times.Once);
        }
    }
}