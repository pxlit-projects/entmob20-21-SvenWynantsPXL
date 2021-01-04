using System;
using Autofac;
using SmartHouseLights.Services;
using SmartHouseLights.Services.Interfaces;
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

            builder.RegisterType<LoginViewModel>();
            builder.RegisterType<LoginView>();
            builder.RegisterType<HomeViewModel>();
            builder.RegisterType<HomeView>();
            builder.RegisterType<LightListViewModel>();
            builder.RegisterType<LightListView>();
            builder.RegisterType<LightDetailsView>();
            builder.RegisterType<LightDetailsViewModel>();
            builder.RegisterType<GroupListView>();
            builder.RegisterType<GroupListViewModel>();
            builder.RegisterType<GroupDetailView>();
            builder.RegisterType<GroupDetailViewModel>();

            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().SingleInstance();
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<LightService>().As<ILightService>().SingleInstance();
            builder.RegisterType<GroupService>().As<IGroupService>().SingleInstance();
            builder.RegisterType<ConnectionFactory>().As<IConnectionFactory>().SingleInstance();
            _container = builder.Build();
        }

        public static object Resolve(Type typeName)
        {
            return _container.Resolve(typeName);
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}