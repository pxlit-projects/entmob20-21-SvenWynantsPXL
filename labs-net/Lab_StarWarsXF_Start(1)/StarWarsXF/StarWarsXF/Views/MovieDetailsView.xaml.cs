using System;
using StarWarsUniverse.Domain;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StarWarsXF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieDetailsView : ContentPage
    {
        //private Movie _selectedMovie;

        public MovieDetailsView()
        {
            InitializeComponent();
            /*MyPlanetsButton = PlanetsButton;

            CheckEnabledUpButton();
            CheckEnabledDownButton();*/
        }

        /*private void CheckEnabledDownButton()
        {
            DownButton.IsEnabled = _selectedMovie?.Rating > 0;
        }

        private void CheckEnabledUpButton()
        {
            UpButton.IsEnabled = _selectedMovie?.Rating < 10;
        }

        public void FillMovieDetails(Movie movie)
        {
            _selectedMovie = movie;

            CheckEnabledUpButton();
            CheckEnabledDownButton();

            var imageFileName = System.Convert.ToString(_selectedMovie.Title).ToLower();
            imageFileName = imageFileName.Replace(' ', '_') + ".jpg";
            PosterImage.Source = imageFileName;

            ReleaseDateLabel.Text = $"{_selectedMovie.ReleaseDate:dd/MM/yyyy}";
            DirectorLabel.Text = _selectedMovie.Director;
            ProducerLabel.Text = _selectedMovie.Producer;
            RatingProgress.Progress = ConvertRating(_selectedMovie.Rating);
        }

        private double ConvertRating(float value)
        {
            double valueOnScaleOfTen = (double)value;
            return valueOnScaleOfTen / 10.0;
        }*/
    }
}