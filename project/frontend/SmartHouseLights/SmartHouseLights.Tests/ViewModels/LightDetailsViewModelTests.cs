using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SmartHouseLights.Domain.Models;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Tests.Builders;
using SmartHouseLights.ViewModels;
using Xamarin.Forms;

namespace SmartHouseLights.Tests.ViewModels
{
    [TestFixture]
    public class LightDetailsViewModelTests
    {
        private Mock<ILightService> _lightServiceMock;
        private Mock<INavigationService> _navServiceMock;
        private Mock<IGroupService> _groupServiceMock;
        private Mock<IAuthenticationService> _authServiceMock;
        private Mock<IAlertService> _alertServiceMock;
        private LightDetailsViewModel _model;

        [SetUp]
        public void Setup()
        {
            _lightServiceMock = new Mock<ILightService>();
            _navServiceMock = new Mock<INavigationService>();
            _groupServiceMock = new Mock<IGroupService>();
            _authServiceMock = new Mock<IAuthenticationService>();
            _alertServiceMock = new Mock<IAlertService>();

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
            _authServiceMock.Setup(a => a.GetUser()).Returns(() => new UserBuilder().WithAdminUser().WithId(1).Build());

            _model = new LightDetailsViewModel(_lightServiceMock.Object, _navServiceMock.Object, _groupServiceMock.Object, _authServiceMock.Object, _alertServiceMock.Object);
        }

        [Test]
        public void OnFlipSwitchShouldTurnLightOnIfLightWasOff()
        {
            _model.Light = new LightBuilder().WithId(1).WithDummy().Build();
            _model.Light.OnState = false;

            _lightServiceMock.Setup(l => l.FlipSwitch(1)).Returns(() =>
            {
                _model.Light.OnState = !_model.Light.OnState;
                return _model.Light;
            });

            _model.FlipSwitchCommand.Execute(null);

            Assert.That(_model.Light.OnState, Is.True);
            _lightServiceMock.Verify(l => l.FlipSwitch(1), Times.Once);
        }

        [Test]
        public void OnFlipSwitchShouldTurnLightOffIfLightWasOn()
        {
            _model.Light = new LightBuilder().WithId(1).WithDummy().Build();
            _model.Light.OnState = true;

            _lightServiceMock.Setup(l => l.FlipSwitch(1)).Returns(() =>
            {
                _model.Light.OnState = !_model.Light.OnState;
                return _model.Light;
            });

            _model.FlipSwitchCommand.Execute(null);

            Assert.That(_model.Light.OnState, Is.False);
            _lightServiceMock.Verify(l => l.FlipSwitch(1), Times.Once);
        }

        [Test]
        public void OnDragCompletedShouldCallUpdateLight()
        {
            _model.Light = new LightBuilder().WithId(1).WithDummy().Build();

            _lightServiceMock.Setup(l => l.UpdateLight(_model.Light)).Returns(() => _model.Light);

            _model.UpdateLightCommand.Execute(null);

            _lightServiceMock.Verify(l => l.UpdateLight(_model.Light), Times.Once);
        }

        [Test]
        public void OnDeleteShouldNavigateToPreviousPageAndEmptyMessage()
        {
            _model.Light = new LightBuilder().WithId(1).Build();
            _alertServiceMock.Setup(a => a.PopupOnDeleteLight()).Returns(() => Task.FromResult(true));
            _lightServiceMock.Setup(l => l.DeleteLightById(1)).Returns(() => true);

            _model.DeleteLightCommand.Execute(null);

            Assert.That(_model.ErrorMessage, Is.EqualTo(""));
            _navServiceMock.Verify(n => n.NavigateToAsync(".."), Times.Once);
        }

        [Test]
        public void OnDeleteShouldSetErrorMessageIfDeleteWentWrong()
        {
            _model.Light = new LightBuilder().WithId(1).Build();
            _alertServiceMock.Setup(a => a.PopupOnDeleteLight()).Returns(() => Task.FromResult(true));
            _lightServiceMock.Setup(l => l.DeleteLightById(1)).Returns(() => false);

            _model.DeleteLightCommand.Execute(null);

            Assert.That(_model.ErrorMessage, Is.EqualTo("Something went wrong deleting the light"));
        }

        [Test]
        public void UserShouldBeSetOnCreationOfModel()
        {
            Assert.That(_model.User, Is.Not.Null);
        }

        [Test]
        public void OnAddGroupShouldSetChosenGroupForLight()
        {
            _model.CurrentGroup = new GroupBuilder().WithLights().WithId(1).Build();
            _model.Light = new LightBuilder().WithId(1).Build();

            _model.AddLightToGroupCommand.Execute(null);

            _groupServiceMock.Verify(g => g.AddLightToGroup(1, 1), Times.Once);
        }

        [Test]
        public void RefreshCommandShouldRefreshLight()
        {
            _model.IsRefreshing = true;
            _model.Light = new LightBuilder().WithId(1).WithDummy().Build();
            Light updatedLight = new LightBuilder().WithId(1).WithDummy().Build();
            updatedLight.Manufacturer = Manufacturer.PHILIPS;
            _lightServiceMock.Setup(l => l.GetLightById(1)).Returns(() => updatedLight);
            Light light = _model.Light;

            _model.RefreshCommand.Execute(null);

            Assert.That(updatedLight.Id, Is.EqualTo(light.Id));
            Assert.That(updatedLight.Manufacturer, Is.Not.EqualTo(light.Manufacturer));
            Assert.That(_model.IsRefreshing, Is.False);
        }

        [Test]
        public void RemoveTimerShouldSetTimerOfLightToEmptyString()
        {
            _model.Light = new LightBuilder().WithId(1).WithDummy().Build();
            _model.Light.OnTimer = "test string";

            _model.RemoveTimerCommand.Execute(null);

            Assert.That(_model.Light.OnTimer, Is.EqualTo(""));
        }

        [Test]
        public void SaveChangesCommandShouldCallUpdateLight()
        {
            _model.Light = new LightBuilder().WithId(1).Build();
            _model.Light.Brightness = 100;
            _lightServiceMock.Setup(l => l.UpdateLight(_model.Light)).Returns(() => _model.Light);

            _model.SaveChangesCommand.Execute(null);

            _lightServiceMock.Verify(l => l.UpdateLight(_model.Light), Times.Once);
        }

        [Test]
        public void GetListIdShouldThrowErrorIfGroupIdDoesNotExist()
        {
            _model.Light = new LightBuilder().WithId(1).WithDummy().Build();
            _lightServiceMock.Setup(l => l.GetLightById(1)).Returns(_model.Light);
            _model.Light.GroupId = 5;
            
            Assert.Throws<InvalidOperationException>(() => _model.RefreshCommand.Execute(null));
        }
    }
}