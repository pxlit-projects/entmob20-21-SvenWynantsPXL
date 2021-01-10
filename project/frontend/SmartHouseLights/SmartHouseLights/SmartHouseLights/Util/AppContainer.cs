using System;
using Autofac;
using SmartHouseLights.Data;
using SmartHouseLights.Data.Services;
using SmartHouseLights.Data.Services.Interfaces;
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
            builder.RegisterType<HomeViewModel>();
            builder.RegisterType<LightListViewModel>();
            builder.RegisterType<LightDetailsViewModel>();
            builder.RegisterType<GroupListViewModel>();
            builder.RegisterType<GroupDetailViewModel>();
            builder.RegisterType<AddLightViewModel>();
            builder.RegisterType<AddGroupViewModel>();
            builder.RegisterType<StatisticsViewModel>();

            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().SingleInstance();
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<LightService>().As<ILightService>().SingleInstance();
            builder.RegisterType<GroupService>().As<IGroupService>().SingleInstance();
            builder.RegisterType<ConnectionFactory>().As<IConnectionFactory>().SingleInstance();
            builder.RegisterType<StatisticsService>().As<IStatisticsService>().SingleInstance();
            builder.RegisterType<UserService>().As<IUserService>().SingleInstance();

            builder.RegisterInstance(SmartHouseContextFactory.Create()).As<SmartHouseContext>();

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