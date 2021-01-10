using Moq;
using NUnit.Framework;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Tests.Builders;
using SmartHouseLights.ViewModels;
using SmartHouseLights.Views;

namespace SmartHouseLights.Tests.ViewModels
{
    [TestFixture]
    public class AddGroupViewModelTests
    {
        private AddGroupViewModel _model;
        private Mock<INavigationService> _navServiceMock;
        private Mock<IGroupService> _groupServiceMock;

        [SetUp]
        public void Setup()
        {
            _navServiceMock = new Mock<INavigationService>();
            _groupServiceMock = new Mock<IGroupService>();

            _model = new AddGroupViewModel(_navServiceMock.Object, _groupServiceMock.Object);
        }

        [Test]
        public void OnSaveGroupShouldSetErrorMessageIfNameIsNothing()
        {
            _model.SaveGroupCommand.Execute(null);

            Assert.That(_model.ErrorMessage, Is.EqualTo("You need to fill in Name"));
            _groupServiceMock.Verify(g => g.AddGroup(_model.GroupModel), Times.Never);
        }

        [Test]
        public void OnSaveGroupShouldSetErrorMessageIfAddingGroupFailed()
        {
            _model.GroupModel.Name = "Testgroup";
            _groupServiceMock.Setup(g => g.AddGroup(_model.GroupModel))
                .Returns(() => null);

            _model.SaveGroupCommand.Execute(null);

            _groupServiceMock.Verify(g => g.AddGroup(_model.GroupModel), Times.Once);
            Assert.That(_model.ErrorMessage, Is.EqualTo("Something went wrong adding the group"));
        }

        [Test]
        public void OnSaveGroupShouldNavigateToListViewOnSuccess()
        {
            _model.GroupModel.Name = "Testgroup";
            _model.ErrorMessage = "Error message";
            _groupServiceMock.Setup(g => g.AddGroup(_model.GroupModel))
                .Returns(() => new GroupBuilder().WithId(1).Build());

            _model.SaveGroupCommand.Execute(null);

            Assert.That(_model.ErrorMessage, Is.EqualTo(""));
            _groupServiceMock.Verify(g => g.AddGroup(_model.GroupModel), Times.Once);
            _navServiceMock.Verify(n => n.NavigateToAsync($"//{nameof(GroupListView)}"));
        }
    }
}