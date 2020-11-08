using StarWarsUniverse.Domain;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StarWarsXF
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : MasterDetailPage
    {
        public MainView()
        {
            InitializeComponent();

            MyMasterPage.MyListView.ItemSelected += MyListView_ItemSelected;
        }

        private void MyListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var movie = (Movie) e.SelectedItem;
            if (movie != null)
            {
                var page = new MovieDetailsView {Title = movie.Title};
                page.FillMovieDetails(movie);

                Detail = new NavigationPage(page);

                IsPresented = false;
            }
        }

    }
}