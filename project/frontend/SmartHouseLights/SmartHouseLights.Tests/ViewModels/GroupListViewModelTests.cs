﻿using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SmartHouseLights.Models;
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

        [SetUp]
        public void Setup()
        {
            _navServiceMock = new Mock<INavigationService>();
            _groupServiceMock = new Mock<IGroupService>();

            List<LightGroup> groups = new List<LightGroup> 
            {
                new GroupBuilder().WithId(1).Build(),
                new GroupBuilder().WithId(2).Build()
            };

            _groupServiceMock.Setup(g => g.GetAllGroups()).Returns(() => groups);

            _model = new GroupListViewModel(_navServiceMock.Object, _groupServiceMock.Object);
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
    }
}