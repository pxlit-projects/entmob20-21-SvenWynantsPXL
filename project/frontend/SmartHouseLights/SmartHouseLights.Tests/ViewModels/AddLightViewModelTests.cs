using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SmartHouseLights.Domain.Models;
using SmartHouseLights.Models;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Tests.Builders;
using SmartHouseLights.ViewModels;
using SmartHouseLights.Views;

namespace SmartHouseLights.Tests.ViewModels
{
    [TestFixture]
    public class AddLightViewModelTests
    {
        private AddLightViewModel _model;
        private Mock<INavigationService> _navServiceMock;
        private Mock<ILightService> _lightServiceMock;
        private Mock<IGroupService> _groupServiceMock;

        [SetUp]
        public void Setup()
        {
            _navServiceMock = new Mock<INavigationService>();
            _lightServiceMock = new Mock<ILightService>();
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

            _model = new AddLightViewModel(_navServiceMock.Object, _lightServiceMock.Object, _groupServiceMock.Object);
        }

        [Test]
        public void OnSaveLightShouldSetErrorMessageWhenFail()
        {
            string errMessage = "All fields need to be entered.";
            
            _model.SaveLightCommand.Execute(null);

            Assert.That(_model.ErrorMessage, Is.EqualTo(errMessage));

            _model.LightModel.Name = "test";

            _model.SaveLightCommand.Execute(null);

            Assert.That(_model.ErrorMessage, Is.EqualTo(errMessage));

            _model.LightModel.Name = "";
            _model.LightModel.Type = "Test";

            _model.SaveLightCommand.Execute(null);

            Assert.That(_model.ErrorMessage, Is.EqualTo(errMessage));
        }

        [Test]
        public void OnSaveLightShouldClearErrorMessageOnSuccess()
        {
            _model.ErrorMessage = "All fields need to be entered.";

            _model.LightModel.Name = "Test";
            _model.LightModel.Type = "Test";

            _model.SaveLightCommand.Execute(null);

            Assert.That(_model.ErrorMessage, Is.EqualTo(""));
        }

        [Test]
        public void OnSaveLightShouldNavigateToListWhenSuccess()
        {
            _model.LightModel.Name = "Test";
            _model.LightModel.Type = "Test";
            _lightServiceMock.Setup(l => l.AddLight(_model.LightModel))
                .Returns(() =>
                {
                    Light light = new LightBuilder().WithId(1).WithDummy().Build();
                    return light;
                });

            _model.SaveLightCommand.Execute(null);
            
            _navServiceMock.Verify(n => n.NavigateToAsync($"//{nameof(LightListView)}"), Times.Once);
            _lightServiceMock.Verify(l => l.AddLight(_model.LightModel), Times.Once);
        }

        [Test]
        public void OnSaveLightShouldSetGroupIdOfSelectedGroup()
        {
            _model.LightModel.Name = "Test";
            _model.LightModel.Type = "Test";
            _model.CurrentGroup = new GroupBuilder().WithId(1).Build();

            _lightServiceMock.Setup(l => l.AddLight(_model.LightModel))
                .Returns(() =>
                {
                    Light light = new LightBuilder().WithId(1).WithDummy().Build();
                    return light;
                });

            _model.SaveLightCommand.Execute(null);

            Assert.That(_model.LightModel.LightGroupId, Is.EqualTo(1));
        }
    }
}