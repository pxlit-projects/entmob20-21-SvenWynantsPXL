using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SmartHouseLights.Data.Services.Interfaces;
using SmartHouseLights.Domain.Models;
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
        private ILightService _lightService;
        private Mock<IConnectionFactory> _connFactoryMock;
        private Mock<IStatisticsService> _statServiceMock;
        private Mock<IAuthenticationService> _authServiceMock;

        [SetUp]
        public void Setup()
        {
            _connFactoryMock = new Mock<IConnectionFactory>();
            _authServiceMock = new Mock<IAuthenticationService>();
            _statServiceMock = new Mock<IStatisticsService>();

            _connFactoryMock.Setup(c => c.GetHttpClient()).Returns(() => new ClientBuilder().Build());
            
            _groupService = new GroupService(_connFactoryMock.Object);
            _lightService = new LightService(_connFactoryMock.Object, _statServiceMock.Object, _authServiceMock.Object);
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

        [Test]
        public void AddLightToGroupShouldAddGivenLightWithIdToGroupWithGivenId()
        {
            LightGroup group = _groupService.GetAllGroups()[0];
            int firstCount = group.Lights.Count;

            Light light = _lightService.GetAllLights()[0];

            LightGroup updatedGroup = _groupService.AddLightToGroup(group.Id, light.Id);
            int secCount = updatedGroup.Lights.Count;

            Assert.That(firstCount + 1, Is.EqualTo(secCount));
        }

        [Test]
        public void DeleteGroupShouldReturnTrueIfSuccess()
        {
            CreateGroupModel model = new CreateGroupModel();
            model.Name = "Testgroup to delete";
            LightGroup group = _groupService.AddGroup(model);

            bool success = _groupService.DeleteGroupById(group.Id);

            Assert.That(group, Is.Not.Null);

            Assert.True(success);
        }

        [Test]
        public void DeleteGroupShouldReturnFalseIfFailed()
        {
            bool success = _groupService.DeleteGroupById(0);

            Assert.That(success, Is.False);
        }
    }
}