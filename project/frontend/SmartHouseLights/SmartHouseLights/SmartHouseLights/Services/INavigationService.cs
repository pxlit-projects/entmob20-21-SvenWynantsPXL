using System.Threading.Tasks;
using SmartHouseLights.ViewModels;

namespace SmartHouseLights.Services
{
    public interface INavigationService
    {
        Task NavigateToAsync(string uri);
    }
}