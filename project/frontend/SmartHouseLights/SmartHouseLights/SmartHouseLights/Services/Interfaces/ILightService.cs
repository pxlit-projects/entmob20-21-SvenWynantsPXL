using System.Collections.Generic;
using SmartHouseLights.Domain.Models;
using SmartHouseLights.Models;

namespace SmartHouseLights.Services.Interfaces
{
    public interface ILightService
    {
        List<Light> GetAllLights();
        Light FlipSwitch(int id);
        Light AddLight(CreateLightModel light);
        Light UpdateLight(Light light);
        Light GetLightById(int lightId);
        bool DeleteLightById(int lightId);
    }
}