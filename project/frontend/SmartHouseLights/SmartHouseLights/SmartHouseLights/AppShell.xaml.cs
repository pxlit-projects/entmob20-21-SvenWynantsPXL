using SmartHouseLights.Views;
using System;
using SmartHouseLights.Services.Interfaces;
using Xamarin.Forms;

namespace SmartHouseLights
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        private readonly INavigationService _service;

        public AppShell(INavigationService service)
        {
            _service = service;
            InitializeComponent();
            Routing.RegisterRoute(nameof(LightDetailsView), typeof(LightDetailsView));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginView");
        }
    }
}
