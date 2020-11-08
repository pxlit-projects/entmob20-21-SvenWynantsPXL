namespace StarWarsXF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MovieListViewModel MovieListViewModel { get; set; }

        public MainViewModel()
        {
            MovieListViewModel = new MovieListViewModel();
        }
    }
}