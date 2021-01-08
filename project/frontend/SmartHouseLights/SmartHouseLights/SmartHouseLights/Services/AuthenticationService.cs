using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartHouseLights.Domain.Models;
using SmartHouseLights.Services.Interfaces;

namespace SmartHouseLights.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConnectionFactory _connectionFactory;
        private static User _user;

        public AuthenticationService(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<User> Login(string name, string password)
        {
            string authString = name + ":" + password;
            string authHeader = "Basic " + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(authString));

            var url = "/users/login";
            var client = _connectionFactory.GetHttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Authorization", authHeader);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(url);

            _user = null;

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                _user = JsonConvert.DeserializeObject<User>(content);
                _connectionFactory.SetAuthenticationHeader(authHeader);
            }

            return _user;
        }

        public User GetUser()
        {
            return _user;
        }
    }
}