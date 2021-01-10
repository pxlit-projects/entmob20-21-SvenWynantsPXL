using SmartHouseLights.Views;
using System;
using SmartHouseLights.Services.Interfaces;
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
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            _connectionFactory.RemoveHeader();
            await _service.NavigateToAsync("//LoginView");
        }
    }
}
