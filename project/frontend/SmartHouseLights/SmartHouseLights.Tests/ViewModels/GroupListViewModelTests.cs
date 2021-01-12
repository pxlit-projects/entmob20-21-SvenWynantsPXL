using System;
using System.Collections.Generic;
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
    public class GroupListViewModelTests
    {
        private GroupListViewModel _model;
        private Mock<INavigationService> _navServiceMock;
        private Mock<IGroupService> _groupServiceMock;
        private Mock<IAuthenticationService> _authServiceMock;

        [SetUp]
        public void Setup()
        {
            _navServiceMock = new Mock<INavigationService>();
            _groupServiceMock = new Mock<IGroupService>();
            _authServiceMock = new Mock<IAuthenticationService>();
            _authServiceMock.Setup(a => a.GetUser())
                .Returns(() => new UserBuilder().WithAdminUser().Build());

            List<LightGroup> groups = new List<LightGroup> 
            {
                new GroupBuilder().WithId(1).WithLights().Build(),
                new GroupBuilder().WithId(2).Build()
            };

            _groupServiceMock.Setup(g => g.GetAllGroups()).Returns(() => groups);

            _model = new GroupListViewModel(_navServiceMock.Object, _groupServiceMock.Object, _authServiceMock.Object);
        }

        [Test]
        public void OnGroupSelectedShouldNavigateToDetailPage()
        {
            _groupServiceMock.Setup(g => g.GetGroupById(1))
                .Returns(() => _model.Groups[0]);

            _model.GroupSelectedCommand.Execute(0);

            _groupServiceMock.Verify(g => g.GetGroupById(1), Times.Once);
            _navServiceMock.Verify(n => n.NavigateToAsync(nameof(GroupDetailView)), Times.Once);
        }

        [Test]
        public void OnFlipPressedShouldTurnAllLightsOfGroupOnIfAllOnStateWasFalse()
        {
            _groupServiceMock.Setup(g => g.TurnAllLightsOnInGroup(1)).Returns(() =>
            {
                LightGroup group = _model.Groups[0];
                
                return group;
            });

            _model.FlipSwitchCommand.Execute(1);

            _groupServiceMock.Verify(g => g.TurnAllLightsOnInGroup(1), Times.Once);
            _groupServiceMock.Verify(g => g.TurnAllLightsOffInGroup(1), Times.Never);
            Assert.That(_model.Groups[0].AllOnState, Is.True);
            Assert.That(_model.Groups[0].HasOnState, Is.True);
        }

        [Test]
        public void OnFlipPressedShouldTurnAllLightsOfGroupOffIfOnStateWasTrue()
        {
            _model.Groups[0].AllOnState = true;
            _model.Groups[0].HasOnState = true;

            _groupServiceMock.Setup(g => g.TurnAllLightsOffInGroup(1))
                .Returns(() => _model.Groups[0]);

            _model.FlipSwitchCommand.Execute(1);

            _groupServiceMock.Verify(g => g.TurnAllLightsOffInGroup(1), Times.Once);
            _groupServiceMock.Verify(g => g.TurnAllLightsOnInGroup(1), Times.Never);

            Assert.That(_model.Groups[0].AllOnState, Is.False);
            Assert.That(_model.Groups[0].HasOnState, Is.False);
        }

        [Test]
        public void RefreshListCommandShouldFillListWithRecentData()
        {
            var user = _model.User;
            _authServiceMock.Setup(a => a.GetUser())
                .Returns(() => new UserBuilder().WithAdminUser().Build());
            _model.Groups = new List<LightGroup>();
            _model.IsRefreshing = true;
            _model.RefreshListCommand.Execute(null);

            Assert.That(_model.Groups.Count, Is.EqualTo(2));
            Assert.That(_model.IsRefreshing, Is.False);
            Assert.That(_model.User.Name, Is.Not.EqualTo(user.Name));
        }

        [Test]
        public void AddGroupCommandShouldNavigateToAddGroupView()
        {
            _model.AddGroupCommand.Execute(null);

            _navServiceMock.Verify(n => n.NavigateToAsync(nameof(AddGroupView)), Times.Once);
        }

        [Test]
        public void OnCanFlipSwitchShouldReturnTrueIfUserGroupsIsNullOrEmpty()
        {
            Assert.That(_model.FlipSwitchCommand.CanExecute(1), Is.True);

            _model.User.Groups = new List<LightGroup>();

            Assert.That(_model.FlipSwitchCommand.CanExecute(1), Is.True);
        }

        [Test]
        public void OnCanFlipSwitchShouldReturnFalseIfGroupLightsAreEmpty()
        {
            Assert.That(_model.FlipSwitchCommand.CanExecute(2), Is.False);
        }

        [Test]
        public void OnCanFlipSwitchShouldReturnFalseIfGroupIsInUserGroupList()
        {
            _model.User.Groups = new List<LightGroup>{_model.Groups[0]};

            Assert.That(_model.FlipSwitchCommand.CanExecute(1), Is.False);
        }

        [Test]
        public void WrongIdOfGroupShouldThrowInvalidOperationError()
        {
            Assert.Throws<InvalidOperationException>(() => _model.FlipSwitchCommand.CanExecute(0));
        }
    }
}