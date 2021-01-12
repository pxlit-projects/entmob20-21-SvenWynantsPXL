using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using SmartHouseLights.Data.Services.Interfaces;
using SmartHouseLights.Domain.Models;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Tests.Builders;
using SmartHouseLights.ViewModels;

namespace SmartHouseLights.Tests.ViewModels
{
    [TestFixture]
    public class StatisticsViewModelTests
    {
        private StatisticsViewModel _model;
        private Mock<IAuthenticationService> _authServiceMock;
        private Mock<IStatisticsService> _statServiceMock;

        [SetUp]
        public void Setup()
        {
            _authServiceMock = new Mock<IAuthenticationService>();
            _statServiceMock = new Mock<IStatisticsService>();
        }

        [Test]
        public void OnCreateModelShouldFillListWithStats()
        {
            var user = new UserBuilder().WithAdminUser().Build();
            _authServiceMock.Setup(a => a.GetUser())
                .Returns(() => user);
            _statServiceMock.Setup(s => s.GetAllStatisticsForUserWithId(user.Id))
                .Returns(() =>
                {
                    var stats = new List<UserLightStatistic>
                    {
                        new UserLightStatisticBuilder().WithLight(1).Build(),
                        new UserLightStatisticBuilder().WithLight(2).Build()
                    };
                    return stats;
                });

            _model = new StatisticsViewModel(_authServiceMock.Object, _statServiceMock.Object);

            Assert.That(_model.Statistics, Is.Not.Null);
            Assert.That(_model.Statistics.Count, Is.EqualTo(2));
        }

        [Test]
        public void RefreshStatsShouldRefreshListWithStats()
        {
            var user = new UserBuilder().WithAdminUser().Build();
            _authServiceMock.Setup(a => a.GetUser())
                .Returns(() => user);
            _statServiceMock.Setup(s => s.GetAllStatisticsForUserWithId(user.Id))
                .Returns(() =>
                {
                    var stats = new List<UserLightStatistic>
                    {
                        new UserLightStatisticBuilder().WithLight(1).Build(),
                        new UserLightStatisticBuilder().WithLight(2).Build()
                    };
                    return stats;
                });

            _model = new StatisticsViewModel(_authServiceMock.Object, _statServiceMock.Object);

            _model.Statistics = new List<UserLightStatistic>();
            _model.IsRefreshing = true;
            Assert.That(_model.Statistics, Is.Empty);

            _authServiceMock.Setup(a => a.GetUser())
                .Returns(() => new UserBuilder().WithAdminUser().Build());

            _model.RefreshStatsCommand.Execute(null);

            Assert.That(_model.IsRefreshing, Is.False);
            Assert.That(_model.Statistics.Count, Is.EqualTo(2));
            Assert.That(_model.User.Name, Is.Not.EqualTo(user.Name));
        }
    }
}