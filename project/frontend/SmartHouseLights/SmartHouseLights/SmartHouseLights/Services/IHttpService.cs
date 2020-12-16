using System.Collections.Generic;
using SmartHouseLights.Models;

namespace SmartHouseLights.Services
{
    public interface IHttpService
    {
        List<Light> GetAllLights(string name, string password);
    }
}