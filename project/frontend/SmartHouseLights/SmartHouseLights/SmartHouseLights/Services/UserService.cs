using System.Collections.Generic;
using SmartHouseLights.Domain.Models;
using SmartHouseLights.Services.Interfaces;

namespace SmartHouseLights.Services
{
    public class UserService : IUserService
    {
        public List<User> GetAllUsers()
        {
            throw new System.NotImplementedException();
        }

        public bool RestrictUserForGroup(int userId, int groupId)
        {
            throw new System.NotImplementedException();
        }

        public bool RemoveRestriction(int userId, int groupId)
        {
            throw new System.NotImplementedException();
        }
    }
}