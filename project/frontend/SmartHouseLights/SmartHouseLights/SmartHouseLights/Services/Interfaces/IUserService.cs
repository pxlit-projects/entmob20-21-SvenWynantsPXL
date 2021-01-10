using System.Collections.Generic;
using SmartHouseLights.Domain.Models;

namespace SmartHouseLights.Services.Interfaces
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        bool RestrictUserForGroup(int userId, int groupId);
        bool RemoveRestriction(int userId, int groupId);
    }
}