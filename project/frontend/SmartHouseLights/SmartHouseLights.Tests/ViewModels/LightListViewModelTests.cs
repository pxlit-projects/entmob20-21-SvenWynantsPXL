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
    public class LightListViewModelTests
    {
        private LightListViewModel _model;
        private Mock<INavigationService> _navServiceMock;
        private Mock<ILightService> _lightServiceMock;
        private Mock<IAuthenticationService> _authServiceMock;

        [SetUp]
        public void Setup()
        {
            _navServiceMock = new Mock<INavigationService>();
            _lightServiceMock = new Mock<ILightService>();
            _authServiceMock = new Mock<IAuthenticationService>();

            List<Light> lights = new List<Light>
            {
                new LightBuilder().WithDummy().WithId(1).Build(),
                new LightBuilder().WithDummy().WithId(2).Build()
            };

            _authServiceMock.Setup(a => a.GetUser()).Returns(() => new UserBuilder().WithAdminUser().WithId(1).Build());

            _lightServiceMock.Setup(l => l.GetAllLights()).Returns(() => lights);
            _model = new LightListViewModel(_navServiceMock.Object, _lightServiceMock.Object, _authServiceMock.Object);
        }

        [Test]
        public void CreateModelShouldSetLights()
        {
            Assert.That(_model.Lights, Is.Not.Null);
            Assert.That(_model.Lights.Count, Is.EqualTo(2));
        }

        [Test]
        public void OnFlipSwitchShouldReturnLightWithFlippedSwitch()
        {
            Light light = _model.Lights[0];

            light.OnState = false;

            _model.FlipSwitchCommand.Execute(1);

            _lightServiceMock.Verify(l => l.FlipSwitch(1), Times.Once);

            Assert.That(light.OnState, Is.True);
        }

        [Test]
        public void RefreshListCommandShouldRefreshExistingList()
        {
            _lightServiceMock.Setup(l => l.GetAllLights()).Returns(() => new List<Light>());

            Assert.That(_model.IsRefreshing, Is.False);
            _model.IsRefreshing = true;
            _model.RefreshListCommand.Execute(null);

            Assert.That(_model.IsRefreshing, Is.False);
            Assert.That(_model.Lights, Is.Empty);
        }

        [Test]
        public void OnLightSelectedShouldNavigateToDetailPage()
        {
            int id = 0;
            Light light = _model.Lights[id];
            _lightServiceMock.Setup(l => l.GetLightById(1)).Returns(() => light);

            _model.LightSelectedCommand.Execute(id);

            _lightServiceMock.Verify(l => l.GetLightById(1), Times.Once);
            _navServiceMock.Verify(n => n.NavigateToAsync(nameof(LightDetailsView)), Times.Once);
        }

        [Test]
        public void OnClickAddShouldNavigateToAddLightView()
        {
            _model.AddLightCommand.Execute(null);

            _navServiceMock.Verify(n => n.NavigateToAsync(nameof(AddLightView)), Times.Once);
        }

        [Test]
        public void OnCanFlipShouldThrowErrorIfIdIsWrong()
        {
            Assert.Throws<InvalidOperationException>(() => _model.FlipSwitchCommand.CanExecute(0));
        }

        [Test]
        public void OnCanFlipAndSelectShouldReturnTrueIfGroupIdOfLightIsZero()
        {
            _model.Lights[0].GroupId = 0;

            Assert.That(_model.FlipSwitchCommand.CanExecute(1), Is.True);
            Assert.That(_model.LightSelectedCommand.CanExecute(0), Is.True);
        }

        [Test]
        public void CheckPrivilegeShouldReturnTrueIfUserGroupsIsNullOrEmpty()
        {
            _model.User.Groups = null;

            Assert.That(_model.FlipSwitchCommand.CanExecute(1), Is.True);
            Assert.That(_model.LightSelectedCommand.CanExecute(0), Is.True);

            _model.User.Groups = new List<LightGroup>();

            Assert.That(_model.FlipSwitchCommand.CanExecute(1), Is.True);
            Assert.That(_model.LightSelectedCommand.CanExecute(0), Is.True);
        }

        [Test]
        public void CheckPrivilegeShouldReturnTrueIfUserDoesNotHaveGroupInList()
        {
            _model.Lights[0].GroupId = 1;
            LightGroup group = new GroupBuilder().WithLights().WithId(2).Build();
            _model.User.Groups = new List<LightGroup> { group };

            Assert.That(_model.FlipSwitchCommand.CanExecute(1), Is.True);
            Assert.That(_model.LightSelectedCommand.CanExecute(0), Is.True);
        }

        [Test]
        public void CheckPrivilegeShouldReturnFalseIfUserHasGroupWithLightInList()
        {
            _model.Lights[0].GroupId = 1;
            LightGroup group = new GroupBuilder().WithLights().WithId(1).Build();
            _model.User.Groups = new List<LightGroup> { group };

            Assert.That(_model.FlipSwitchCommand.CanExecute(1), Is.False);
            Assert.That(_model.LightSelectedCommand.CanExecute(0), Is.False);
        }

        [Test]
        public void CheckPrivilegeShouldReturnTrueIfUserIsNull()
        {
            _model.User = null;
            _model.Lights[0].GroupId = 1;

            Assert.That(_model.FlipSwitchCommand.CanExecute(1), Is.True);
            Assert.That(_model.LightSelectedCommand.CanExecute(0), Is.True);
        }
    }
}