using System.Threading.Tasks;
using SmartHouseLights.Domain.Models;

namespace SmartHouseLights.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<User> Login(string name, string password);
        User GetUser();
    }
}