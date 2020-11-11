using System;
using Autofac;
using StarWarsUniverse.Data;
using StarWarsUniverse.Data.Repositories;
using StarWarsUniverse.Data.Repositories.Db;
using StarWarsXF.Services;
using StarWarsXF.ViewModels;

namespace StarWarsXF.Util
{
    public static class AppContainer
    {
        private static IContainer _container;

        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            //ViewModels
            builder.RegisterType<MainViewModel>().SingleInstance();
            builder.RegisterType<MovieListViewModel>().SingleInstance();
            builder.RegisterType<MovieDetailsViewModel>().SingleInstance();
            builder.RegisterType<PlanetsViewModel>().SingleInstance();

            //Services
            builder.RegisterType<NavigationService>().As<INavigationService>();

            //General
            builder.RegisterInstance(StarWarsContextFactory.Create()).As<StarWarsContext>();
            builder.RegisterType<MovieDbRepository>().As<IMovieRepository>();

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