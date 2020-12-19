using System.Threading.Tasks;
using SmartHouseLights.Models;

namespace SmartHouseLights.Services
{
    public interface IAuthenticationService
    {
        Task<User> Login(string name, string password);
        string GetHeader();
        User GetUser();
    }
}