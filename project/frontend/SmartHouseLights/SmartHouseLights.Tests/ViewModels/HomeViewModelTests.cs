using Moq;
using NUnit.Framework;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.ViewModels;
using SmartHouseLights.Views;

namespace SmartHouseLights.Tests.ViewModels
{
    [TestFixture]
    public class HomeViewModelTests
    {
        private HomeViewModel _homeViewModel;
        private Mock<INavigationService> _navServiceMock;

        [SetUp]
        public void Setup()
        {
            _navServiceMock = new Mock<INavigationService>();
            _homeViewModel = new HomeViewModel(_navServiceMock.Object);
        }

        [Test]
        public void GoToStatisticPageShouldNavigate()
        {
            Assert.That(_homeViewModel.Title, Is.EqualTo("Welcome"));
            _homeViewModel.GoToStatisticsCommand.Execute(null);

            _navServiceMock.Verify(n => n.NavigateToAsync($"//{nameof(StatisticsView)}"), Times.Once);
        }
    }
}