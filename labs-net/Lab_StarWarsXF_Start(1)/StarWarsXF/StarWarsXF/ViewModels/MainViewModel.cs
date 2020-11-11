using StarWarsUniverse.Data.Repositories;
using StarWarsXF.Services;
using StarWarsXF.Util;

namespace StarWarsXF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MovieListViewModel MovieListViewModel { get; set; }

        public MainViewModel()
        {
            MovieListViewModel = new MovieListViewModel(AppContainer.Resolve<IMovieRepository>(), AppContainer.Resolve<INavigationService>());
        }
    }
}