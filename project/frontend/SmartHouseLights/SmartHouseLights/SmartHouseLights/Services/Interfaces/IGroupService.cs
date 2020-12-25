using System.Collections.Generic;
using SmartHouseLights.Models;

namespace SmartHouseLights.Services.Interfaces
{
    public interface IGroupService
    {
        List<LightGroup> GetAllGroups();
        LightGroup TurnAllLightsOnInGroup(int groupId);
        LightGroup TurnAllLightsOffInGroup(int groupId);
    }
}