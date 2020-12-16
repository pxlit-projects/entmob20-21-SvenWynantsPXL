using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHouseLights.Util;
using SmartHouseLights.ViewModels;
using SmartHouseLights.Views;
using Xamarin.Forms;

namespace SmartHouseLights.Services
{
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<Type, Type> _viewModelViewMappings;

        public NavigationService()
        {
            _viewModelViewMappings = new Dictionary<Type, Type>
            {
                {typeof(HomeViewModel), typeof(HomeView)},
                {typeof(LightListViewModel), typeof(LightListView)}
            };
        }

        public async Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            if (!(Application.Current.MainPage is AppShell shell))
            {
                throw new InvalidOperationException("The shell should be the MainPage");
            }

            var view = CreateViewFor(typeof(TViewModel));

            await Shell.Current.GoToAsync(view);
        }

        private string CreateViewFor(Type viewModelType)
        {
            var viewType = GetViewTypeForViewModel(viewModelType);
            //Page view = AppContainer.Resolve(viewType) as Page;
            string view = viewType.Name;

            return view;
        }

        protected Type GetViewTypeForViewModel(Type viewModelType)
        {
            if (!_viewModelViewMappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No match view type was found for ${viewModelType}");
            }

            return _viewModelViewMappings[viewModelType];
        }
    }
}