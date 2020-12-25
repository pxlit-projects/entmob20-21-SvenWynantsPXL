using System.Collections.Generic;
using SmartHouseLights.Models;
using SmartHouseLights.Services.Interfaces;

namespace SmartHouseLights.Services
{
    public class GroupService : IGroupService
    {
        public List<LightGroup> GetAllGroups()
        {
            throw new System.NotImplementedException();
        }

        public LightGroup TurnAllLightsOnInGroup(int groupId)
        {
            throw new System.NotImplementedException();
        }

        public LightGroup TurnAllLightsOffInGroup(int groupId)
        {
            throw new System.NotImplementedException();
        }
    }
}