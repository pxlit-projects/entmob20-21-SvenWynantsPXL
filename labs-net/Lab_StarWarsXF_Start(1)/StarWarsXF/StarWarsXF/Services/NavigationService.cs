using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using StarWarsXF.Util;
using StarWarsXF.ViewModels;
using StarWarsXF.Views;
using Xamarin.Forms;

namespace StarWarsXF.Services
{
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<Type, Type> _viewModelViewMappings;

        public NavigationService()
        {
            _viewModelViewMappings = new Dictionary<Type, Type>
            {
                {typeof(MovieDetailsViewModel), typeof(MovieDetailsView)},
                {typeof(PlanetsViewModel), typeof(PlanetsView)}
            };
        }

        public async Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            if (!(Application.Current.MainPage is MainView mainView))
            {
                throw new InvalidOperationException("The MainPage should be the MainView when navigating");
            }

            var view = CreateViewFor(typeof(TViewModel));

            if (mainView.Detail is NavigationPage navigationPage)
            {
                var currentView = navigationPage.CurrentPage;

                if (currentView.GetType() != view.GetType())
                {
                    await navigationPage.PushAsync(view);
                }
            }
            else
            {
                navigationPage = new NavigationPage(view);
                mainView.Detail = navigationPage;
            }

            mainView.IsPresented = false;
        }

        private Page CreateViewFor(Type viewModelType)
        {
            var viewType = GetViewTypeForViewModel(viewModelType);
            //Page view = AppContainer.Resolve(viewType) as Page;
            Page view = Activator.CreateInstance(viewType) as Page;
            object viewModel = AppContainer.Resolve(viewModelType);
            view.BindingContext = viewModel;

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