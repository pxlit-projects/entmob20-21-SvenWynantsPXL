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

        [Test]
        public void GetGroupByIdShouldReturnGroupWithGivenId()
        {
            LightGroup group = _groupService.GetGroupById(1);

            Assert.That(group, Is.Not.Null);
            Assert.That(group.Id, Is.EqualTo(1));
        }

        [Test]
        public void TurnAllLightsOnInGroupShouldReturnGroupWithAllLightsOn()
        {
            LightGroup group = _groupService.TurnAllLightsOffInGroup(1);

            LightGroup groupOn = _groupService.TurnAllLightsOnInGroup(1);

            Assert.That(group.AllOnState, Is.False);
            Assert.That(groupOn.AllOnState, Is.True);
            Assert.That(group.Id, Is.EqualTo(groupOn.Id));
        }

        [Test]
        public void TurnAllLightsOffInGroupShouldReturnGroupWithAllLightsOff()
        {
            LightGroup group = _groupService.TurnAllLightsOnInGroup(1);
            LightGroup groupOff = _groupService.TurnAllLightsOffInGroup(1);

            Assert.That(group.AllOnState, Is.True);
            Assert.That(groupOff.AllOnState, Is.False);
            Assert.That(group.Id, Is.EqualTo(groupOff.Id));
        }

        [Test]
        public void AddGroupShouldReturnNewGroupWithGivenName()
        {
            CreateGroupModel model = new CreateGroupModel {Name = "Testgroup"};
            LightGroup group = _groupService.AddGroup(model);

            Assert.That(group, Is.Not.Null);
            Assert.That(group.Name, Is.EqualTo(model.Name));
        }

        [Test]
        public void AddGroupShouldReturnNullWhenGroupAlreadyExists()
        {
            CreateGroupModel model = new CreateGroupModel {Name = "Testgroup 2"};
            LightGroup group = _groupService.AddGroup(model);
            LightGroup groupFail = _groupService.AddGroup(model);

            Assert.That(group, Is.Not.Null);
            Assert.That(groupFail, Is.Null);
        }
    }
}