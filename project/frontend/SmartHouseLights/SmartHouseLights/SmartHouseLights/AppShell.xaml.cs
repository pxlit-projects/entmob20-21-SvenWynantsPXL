using SmartHouseLights.Views;
using System;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Util;
using Xamarin.Forms;

namespace SmartHouseLights
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        private readonly INavigationService _service;
        private readonly IConnectionFactory _connectionFactory;

        public AppShell(INavigationService service, IConnectionFactory connectionFactory)
        {
            _service = service;
            _connectionFactory = connectionFactory;
            InitializeComponent();
            Routing.RegisterRoute(nameof(LightDetailsView), typeof(LightDetailsView));
            Routing.RegisterRoute(nameof(AddLightView), typeof(AddLightView));
            Routing.RegisterRoute(nameof(GroupDetailView), typeof(GroupDetailView));
            Routing.RegisterRoute(nameof(AddGroupView), typeof(AddGroupView));
            Routing.RegisterRoute(nameof(UserManagementView), typeof(UserManagementView));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            _connectionFactory.RemoveHeader();
            await _service.NavigateToAsync("//LoginView");
            Application.Current.MainPage = new AppShell(AppContainer.Resolve<INavigationService>(), AppContainer.Resolve<IConnectionFactory>());
        }
    }
}
