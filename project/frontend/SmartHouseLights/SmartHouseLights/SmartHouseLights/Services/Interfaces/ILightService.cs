using System.Collections.Generic;
using SmartHouseLights.Models;

namespace SmartHouseLights.Services.Interfaces
{
    public interface ILightService
    {
        List<Light> GetAllLights();
        Light FlipSwitch(int id);
        Light AddLight(CreateLightModel light);
        Light UpdateLight(Light light);
    }
}