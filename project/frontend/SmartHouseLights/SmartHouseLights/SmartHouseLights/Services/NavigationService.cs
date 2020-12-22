using System.Threading.Tasks;
using SmartHouseLights.Services.Interfaces;
using SmartHouseLights.Views;
using Xamarin.Forms;

namespace SmartHouseLights.Services
{
    public class NavigationService : INavigationService
    {
        public async Task NavigateToAsync(string uri)
        {
            await Shell.Current.GoToAsync(uri);
        }
    }
}