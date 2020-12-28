using System.Collections.Generic;
using System.Net.Http;
using Moq;
using NUnit.Framework;
using SmartHouseLights.Models;
using SmartHouseLights.Services;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Tests.Builders;

namespace SmartHouseLights.Tests.Services
{
    [TestFixture]
    public class GroupServiceTests
    {
        private IGroupService _groupService;
        private Mock<IConnectionFactory> _connFactoryMock;

        [SetUp]
        public void Setup()
        {
            _connFactoryMock = new Mock<IConnectionFactory>();
            _connFactoryMock.Setup(c => c.GetHttpClient()).Returns(() => new ClientBuilder().Build());
            _groupService = new GroupService(_connFactoryMock.Object);
        }

        [Test]
        public void GetAllGroupsShouldReturnAllGroupsFromDatabase()
        {
            List<LightGroup> groups = _groupService.GetAllGroups();

            Assert.That(groups, Is.Not.Null);
            Assert.That(groups.Count, Is.GreaterThan(2));
        }
    }
}