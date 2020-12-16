using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartHouseLights.Services
{
    public class NavigationService : INavigationService
    {
        public NavigationService()
        {
        }

        public async Task NavigateToAsync(string uri)
        {
            await Shell.Current.GoToAsync(uri);
        }
    }
}