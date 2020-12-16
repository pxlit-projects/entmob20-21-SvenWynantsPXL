using System;
using Autofac;
using SmartHouseLights.Services;
using SmartHouseLights.ViewModels;
using SmartHouseLights.Views;

namespace SmartHouseLights.Util
{
    public static class AppContainer
    {
        private static IContainer _container;

        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<LoginViewModel>().SingleInstance();
            builder.RegisterType<HomeViewModel>().SingleInstance();
            builder.RegisterType<LightListViewModel>().SingleInstance();
            builder.RegisterType<LightListView>().SingleInstance();

            builder.RegisterType<NavigationService>().As<INavigationService>();
            _container = builder.Build();
        }
    }
}