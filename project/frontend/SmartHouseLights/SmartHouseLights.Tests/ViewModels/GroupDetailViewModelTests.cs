using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Moq;
using NUnit.Framework;
using SmartHouseLights.Domain.Models;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Tests.Builders;
using SmartHouseLights.ViewModels;

namespace SmartHouseLights.Tests.ViewModels
{
    [TestFixture]
    public class GroupDetailViewModelTests
    {
        private GroupDetailViewModel _model;
        private Mock<IGroupService> _groupServiceMock;
        private Mock<INavigationService> _navServiceMock;
        private Mock<IAuthenticationService> _authServiceMock;
        private Mock<IAlertService> _alertServiceMock;

        [SetUp]
        public void Setup()
        {
            _groupServiceMock = new Mock<IGroupService>();
            _navServiceMock = new Mock<INavigationService>();
            _authServiceMock = new Mock<IAuthenticationService>();
            _alertServiceMock = new Mock<IAlertService>();

            _authServiceMock.Setup(a => a.GetUser())
                .Returns(() => new UserBuilder().WithAdminUser().Build());

            _model = new GroupDetailViewModel(_groupServiceMock.Object, _navServiceMock.Object, _authServiceMock.Object, _alertServiceMock.Object);
        }

        [Test]
        public void OnFlipSwitchShouldReturnGroupWithAllLightsOnIfAllOnStateWasOff()
        {
            _model.Group = new GroupBuilder().WithId(1).WithLights().Build();
            _groupServiceMock.Setup(g => g.TurnAllLightsOnInGroup(1))
                .Returns(() => _model.Group);
            
            _model.FlipSwitchCommand.Execute(null);

            _groupServiceMock.Verify(g => g.TurnAllLightsOnInGroup(1), Times.Once);
            _groupServiceMock.Verify(g => g.TurnAllLightsOffInGroup(1), Times.Never);
            Assert.That(_model.Group.AllOnState, Is.True);
            Assert.That(_model.Group.HasOnState, Is.True);

            foreach (var light in _model.Group.Lights)
            {
                Assert.That(light.OnState, Is.True);
            }
        }

        [Test]
        public void OnFlipSwitchShouldReturnGroupWithAllLightsOffIfAllOnStateWasOn()
        {
            _model.Group = new GroupBuilder().WithId(1).WithLights().Build();
            _model.Group.AllOnState = true;
            _model.Group.HasOnState = true;
            foreach (var light in _model.Group.Lights)
            {
                light.OnState = true;
            }

            _groupServiceMock.Setup(g => g.TurnAllLightsOffInGroup(1))
                .Returns(() => _model.Group);

            _model.FlipSwitchCommand.Execute(null);

            _groupServiceMock.Verify(g => g.TurnAllLightsOffInGroup(1), Times.Once);
            _groupServiceMock.Verify(g => g.TurnAllLightsOnInGroup(1), Times.Never);

            Assert.That(_model.Group.AllOnState, Is.False);
            Assert.That(_model.Group.HasOnState, Is.False);
            foreach (var light in _model.Group.Lights)
            {
                Assert.That(light.OnState, Is.False);
            }
        }

        [Test]
        public void OnFlipSwitchCannotExecuteWhenGroupIsNullOrHasNoLights()
        {
            Assert.That(_model.FlipSwitchCommand.CanExecute(null), Is.False);

            _model.Group = new GroupBuilder().WithId(1).Build();

            Assert.That(_model.FlipSwitchCommand.CanExecute(null), Is.False);

            _model.Group.Lights = new List<Light>();
            Assert.That(_model.ErrorMessage, Is.EqualTo("There are no lights to turn on"));
            Assert.That(_model.FlipSwitchCommand.CanExecute(null), Is.False);
        }

        [Test]
        public void OnFlipSwitchCanExecuteWhenGroupHasLights()
        {
            _model.User = new UserBuilder().Build();
            _model.Group = new GroupBuilder().WithId(1).WithLights().Build();

            Assert.That(_model.FlipSwitchCommand.CanExecute(null), Is.True);
        }

        [Test]
        public void OnCanFlipShouldReturnTrueIfUserHasGroupsButCurrentGroup()
        {
            LightGroup denialGroup = new GroupBuilder().WithLights().WithId(2).Build();
            _model.User = new UserBuilder().WithAdminUser().WithId(1).Build();
            _model.Group = new GroupBuilder().WithLights().WithId(1).Build();
            _model.User.Groups = new List<LightGroup>{denialGroup};

            Assert.That(_model.FlipSwitchCommand.CanExecute(null), Is.True);
            Assert.That(_model.DeleteGroupCommand.CanExecute(null), Is.True);
        }

        [Test]
        public void OnCanFlipSwitchShouldReturnTrueWhenGroupHasLights()
        {
            _model.User = new UserBuilder().Build();
            _model.Group = new GroupBuilder().WithId(1).WithLights().Build();

            Assert.That(_model.ErrorMessage, Is.EqualTo(""));
            Assert.That(_model.OnCanFlipSwitch(), Is.True);
        }

        [Test]
        public void OnCanFlipSwitchShouldReturnFalseWhenUserHasGroupInList()
        {
            _model.User = new UserBuilder().Build();
            _model.Group = new GroupBuilder().WithId(1).WithLights().Build();
            _model.User.Groups = new List<LightGroup>{_model.Group};

            Assert.That(_model.OnCanFlipSwitch(), Is.False);
            Assert.That(_model.ErrorMessage, Is.EqualTo("You may not access this group"));
        }

        [Test]
        public void OnCanFlipSwitchShouldCheckForDeleteCommand()
        {
            _model.Group = null;
            Assert.That(_model.DeleteGroupCommand.CanExecute(null), Is.False);

            _model.Group = new GroupBuilder().WithId(1).Build();
            _model.User = null;

            Assert.That(_model.DeleteGroupCommand.CanExecute(null), Is.False);
            Assert.That(_model.ErrorMessage, Is.EqualTo("There are no lights to turn on"));
        }

        [Test]
        public void DeleteCannotExecuteIfUserHasGroupInGroups()
        {
            _model.Group = new GroupBuilder().WithId(1).WithLights().Build();
            _model.User.Groups = new List<LightGroup>{_model.Group};

            Assert.That(_model.DeleteGroupCommand.CanExecute(null), Is.False);
        }

        [Test]
        public void OnDeleteShouldDeleteIfConfirmedPopUp()
        {
            _model.Group = new GroupBuilder().WithId(1).Build();
            _alertServiceMock.Setup(a => a.PopupOnDeleteGroup()).Returns(() => Task.FromResult(true));
            _groupServiceMock.Setup(g => g.DeleteGroupById(1)).Returns(() => true);

            _model.DeleteGroupCommand.Execute(null);

            Assert.That(_model.ErrorMessage, Is.EqualTo(""));
            _navServiceMock.Verify(n => n.NavigateToAsync(".."), Times.Once);
            _groupServiceMock.Verify(g => g.DeleteGroupById(1), Times.Once);
        }

        [Test]
        public void OnDeleteShouldDoNothingIfCanceledPopUp()
        {
            _model.Group = new GroupBuilder().WithId(1).Build();
            _alertServiceMock.Setup(a => a.PopupOnDeleteGroup()).Returns(() => Task.FromResult(false));

            _model.DeleteGroupCommand.Execute(null);

            _navServiceMock.Verify(n => n.NavigateToAsync(".."), Times.Never);
            _groupServiceMock.Verify(g => g.DeleteGroupById(1), Times.Never);
        }

        [Test]
        public void OnDeleteShouldSetErrorMessageIfDeleteDidNotWork()
        {
            _model.Group = new GroupBuilder().WithId(1).Build();
            _alertServiceMock.Setup(a => a.PopupOnDeleteGroup()).Returns(() => Task.FromResult(true));
            _groupServiceMock.Setup(g => g.DeleteGroupById(1)).Returns(() => false);

            _model.DeleteGroupCommand.Execute(null);

            Assert.That(_model.ErrorMessage, Is.EqualTo("Something went wrong deleting the group"));
            _navServiceMock.Verify(n => n.NavigateToAsync(".."), Times.Never);
        }
    }
}