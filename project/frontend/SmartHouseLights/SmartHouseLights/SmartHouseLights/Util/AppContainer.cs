﻿using System;
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

            builder.RegisterType<LoginViewModel>().SingleInstance();
            builder.RegisterType<LoginView>().SingleInstance();
            builder.RegisterType<HomeViewModel>().SingleInstance();
            builder.RegisterType<HomeView>().SingleInstance();
            builder.RegisterType<LightListViewModel>().SingleInstance();
            builder.RegisterType<LightListView>().SingleInstance();
            builder.RegisterType<LightDetailsView>().SingleInstance();
            builder.RegisterType<LightDetailsViewModel>().SingleInstance();
            builder.RegisterType<GroupListView>().SingleInstance();
            builder.RegisterType<GroupListViewModel>().SingleInstance();
            builder.RegisterType<GroupDetailView>().SingleInstance();
            builder.RegisterType<GroupDetailViewModel>().SingleInstance();

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