using System.Threading.Tasks;
using SmartHouseLights.ViewModels;

namespace SmartHouseLights.Services.Interfaces
{
    public interface INavigationService
    {
        Task NavigateToAsync(string uri);
    }
}