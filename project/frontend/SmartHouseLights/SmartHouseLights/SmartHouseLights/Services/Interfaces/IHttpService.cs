using System.Collections.Generic;
using SmartHouseLights.Models;

namespace SmartHouseLights.Services.Interfaces
{
    public interface IHttpService
    {
        List<Light> GetAllLights();
    }
}