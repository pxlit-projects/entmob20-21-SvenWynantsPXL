using System;
using SmartHouseLights.Domain.Models;

namespace SmartHouseLights.Tests.Builders
{
    public class UserBuilder
    {
        private readonly User _user;

        public UserBuilder()
        {
            _user = new User {Id = 1};
        }

        public UserBuilder WithAdminUser()
        {
            _user.Name = Guid.NewGuid().ToString();
            _user.Role = "ROLE_ADMIN";
            return this;
        }

        public User Build()
        {
            return _user;
        }
    }
}