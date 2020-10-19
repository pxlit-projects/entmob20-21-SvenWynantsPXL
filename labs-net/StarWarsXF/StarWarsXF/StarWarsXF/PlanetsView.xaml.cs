using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarWarsUniverse.Domain;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StarWarsXF
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlanetsView : ContentPage
    {
        public PlanetsView()
        {
            InitializeComponent();
        }

        internal void FillPlanetDetails(Movie movie)
        {
            PlanetsListView.ItemsSource = movie.MoviePlanets;
        }
    }
}