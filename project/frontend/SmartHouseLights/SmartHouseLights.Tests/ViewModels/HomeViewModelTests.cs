using Moq;
using NUnit.Framework;
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
        private Mock<INavigationService> _navServiceMock;
        private Mock<IAuthenticationService> _authServiceMock;

        [SetUp]
        public void Setup()
        {
            _navServiceMock = new Mock<INavigationService>();
            _authServiceMock = new Mock<IAuthenticationService>();
            _authServiceMock.Setup(a => a.GetUser())
                .Returns(() => new UserBuilder().WithAdminUser().Build());
            _homeViewModel = new HomeViewModel(_navServiceMock.Object, _authServiceMock.Object);
        }

        [Test]
        public void GoToStatisticPageShouldNavigateToStatisticsView()
        {
            _homeViewModel.GoToStatisticsCommand.Execute(null);

            _navServiceMock.Verify(n => n.NavigateToAsync($"//{nameof(StatisticsView)}"), Times.Once);
        }

        [Test]
        public void GoToUserManagementShouldNavigateToUserManagementView()
        {
            _homeViewModel.GoToUserManagementCommand.Execute(null);

            _navServiceMock.Verify(n => n.NavigateToAsync($"{nameof(UserManagementView)}"), Times.Once);
        }

        [Test]
        public void OnAdminShouldReturnFalseIfUserIsNoAdmin()
        {
            _authServiceMock.Setup(a => a.GetUser())
                .Returns(() => new UserBuilder().WithRegularUser().Build());

            Assert.False(_homeViewModel.GoToUserManagementCommand.CanExecute(null));
        }

        [Test]
        public void OnAdminShouldReturnTrueIfUserIsAdmin()
        {
            Assert.True(_homeViewModel.GoToUserManagementCommand.CanExecute(null));
        }

        [Test]
        public void UserShouldBeSetOnCreation()
        {
            Assert.That(_homeViewModel.User, Is.Not.Null);
        }
    }
}