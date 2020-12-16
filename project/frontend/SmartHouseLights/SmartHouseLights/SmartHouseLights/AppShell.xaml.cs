using SmartHouseLights.ViewModels;
using SmartHouseLights.Views;
using System;
using System.Collections.Generic;
using SmartHouseLights.Services;
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
            Routing.RegisterRoute(nameof(LightListView), typeof(LightListView));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginView");
        }

        private async void GoToLights(object sender, EventArgs e)
        {
            await _service.NavigateToAsync(nameof(LightListView));
        }
    }
}
