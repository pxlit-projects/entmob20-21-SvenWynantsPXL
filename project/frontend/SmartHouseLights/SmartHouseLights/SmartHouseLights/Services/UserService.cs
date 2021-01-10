using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using SmartHouseLights.Domain.Models;
using SmartHouseLights.Services.Interfaces;

namespace SmartHouseLights.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _client;

        public UserService(IConnectionFactory connectionFactory)
        {
            _client = connectionFactory.GetHttpClient();
        }
        public List<User> GetAllUsers()
        {
            var url = "/users/getUsers";

            HttpResponseMessage response = _client.GetAsync(url).Result;

            List<User> users = new List<User>();

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                users = JsonConvert.DeserializeObject<List<User>>(content);
            }

            return users;
        }

        public bool RestrictUserForGroup(int userId, int groupId)
        {
            var url = $"/users/{userId}/addRestriction/{groupId}";

            HttpResponseMessage response = _client.PutAsync(url, null).Result;

            return response.IsSuccessStatusCode;
        }

        public bool RemoveRestriction(int userId, int groupId)
        {
            var url = $"/users/{userId}/removeRestriction/{groupId}";

            HttpResponseMessage response = _client.PutAsync(url, null).Result;

            return response.IsSuccessStatusCode;
        }

        public User FindUserById(int userId)
        {
            var url = $"/users/{userId}";

            HttpResponseMessage response = _client.GetAsync(url).Result;

            User user = null;

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<User>(content);
            }

            return user;
        }
    }
}