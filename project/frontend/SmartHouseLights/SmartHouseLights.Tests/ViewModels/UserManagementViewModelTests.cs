using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SmartHouseLights.Domain.Models;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Tests.Builders;
using SmartHouseLights.ViewModels;

namespace SmartHouseLights.Tests.ViewModels
{
    [TestFixture]
    public class UserManagementViewModelTests
    {
        private UserManagementViewModel _model;
        private Mock<IUserService> _userServiceMock;
        private Mock<IGroupService> _groupServiceMock;

        [SetUp]
        public void Setup()
        {
            _userServiceMock = new Mock<IUserService>();
            _groupServiceMock = new Mock<IGroupService>();

            _groupServiceMock.Setup(g => g.GetAllGroups())
                .Returns(() =>
                {
                    List<LightGroup> groups = new List<LightGroup>
                    {
                        new GroupBuilder().WithId(1).Build(),
                        new GroupBuilder().WithId(2).Build()
                    };
                    return groups;
                });
            _userServiceMock.Setup(u => u.GetAllUsers())
                .Returns(() =>
                {
                    List<User> users = new List<User>
                    {
                        new UserBuilder().WithId(1).WithAdminUser().Build(),
                        new UserBuilder().WithId(2).WithRegularUser().Build()
                    };
                    return users;
                });
            _userServiceMock.Setup(u => u.FindUserById(It.IsAny<int>())).Returns(() => new UserBuilder().WithAdminUser().WithId(1).Build());

            _model = new UserManagementViewModel(_userServiceMock.Object, _groupServiceMock.Object);
        }

        [Test]
        public void OnCreateShouldHaveEnabledAndDisabledListsOnNull()
        {
            Assert.That(_model.EnabledGroups, Is.Null);
            Assert.That(_model.DisabledGroups, Is.Null);
        }

        [Test]
        public void OnUserSelectedShouldReturnFalseIfNoCurrentUserIsSet()
        {
            Assert.That(_model.DisableGroupCommand.CanExecute(1), Is.False);
            Assert.That(_model.EnableGroupCommand.CanExecute(1), Is.False);
        }

        [Test]
        public void CreationOfModelShouldFillUsersAndGroups()
        {
            Assert.That(_model.Groups, Is.Not.Null);
            Assert.That(_model.Users, Is.Not.Null);
        }

        [Test]
        public void EnableGroupShouldCreateEmptyDisabledGroupsListIfUserHasNoGroups()
        {
            var user = new UserBuilder().WithId(1).WithAdminUser().Build();
            _model.CurrentUser = user;

            _model.EnableGroupCommand.Execute(1);
            _userServiceMock.Verify(u => u.RemoveRestriction(1, 1), Times.Once);
            Assert.That(_model.DisabledGroups.Count, Is.EqualTo(0));
            Assert.That(_model.EnabledGroups.Count, Is.EqualTo(2));
        }

        [Test]
        public void DisableAndEnableGroupShouldCheckIfUserSelected()
        {
            _model.CurrentUser = _model.Users[0];

            Assert.True(_model.DisableGroupCommand.CanExecute(1));
            Assert.True(_model.EnableGroupCommand.CanExecute(1));
        }

        [Test]
        public void DisableGroupShouldCallRestrictUser()
        {
            _model.CurrentUser = _model.Users[0];
            
            _model.DisableGroupCommand.Execute(1);

            _userServiceMock.Verify(u => u.RestrictUserForGroup(1, 1), Times.Once);
        }

        [Test]
        public void DisableGroupShouldAddGroupToDisabledGroupList()
        {
            var user = new UserBuilder().WithAdminUser().WithId(1).Build();
            var lightGroup = new GroupBuilder().WithLights().WithId(1).Build();
            _userServiceMock.Setup(u => u.FindUserById(1))
                .Returns(() =>
                {
                    user.Groups = new List<LightGroup> {lightGroup};
                    return user;
                });

            _model = new UserManagementViewModel(_userServiceMock.Object, _groupServiceMock.Object);

            _model.CurrentUser = user;

            _model.DisableGroupCommand.Execute(1);

            Assert.That(_model.DisabledGroups.Count, Is.EqualTo(1));
            Assert.That(_model.EnabledGroups.Count, Is.EqualTo(1));
        }
    }
}