using System;
using SmartHouseLights.Domain.Models;

namespace SmartHouseLights.Tests.Builders
{
    public class UserBuilder
    {
        private readonly User _user;

        public UserBuilder()
        {
            _user = new User {Name = Guid.NewGuid().ToString()};
        }

        public UserBuilder WithId(int id)
        {
            _user.Id = id;
            return this;
        }

        public UserBuilder WithAdminUser()
        {
            _user.Role = "ROLE_ADMIN";
            return this;
        }

        public UserBuilder WithRegularUser()
        {
            _user.Name = Guid.NewGuid().ToString();
            _user.Role = "ROLE_USER";
            return this;
        }

        public User Build()
        {
            return _user;
        }
    }
}