using System.Threading.Tasks;
using SmartHouseLights.Models;

namespace SmartHouseLights.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<User> Login(string name, string password);
        string GetHeader();
        User GetUser();
    }
}