using SmartHouseLights.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace SmartHouseLights.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}