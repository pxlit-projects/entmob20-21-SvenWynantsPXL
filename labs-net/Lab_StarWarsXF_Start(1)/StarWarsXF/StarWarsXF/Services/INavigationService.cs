using System.Threading.Tasks;
using StarWarsXF.ViewModels;

namespace StarWarsXF.Services
{
    public interface INavigationService
    {
        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;
    }
}