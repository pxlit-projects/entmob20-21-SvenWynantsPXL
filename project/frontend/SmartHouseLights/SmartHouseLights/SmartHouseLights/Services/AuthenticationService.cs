using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartHouseLights.Models;
using SmartHouseLights.Services.Interfaces;

namespace SmartHouseLights.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private string baseUrl = "http://192.168.1.19:8080";
        private readonly HttpClient _client;
        private static string _authHeader;
        private static User _user;

        public AuthenticationService()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
        }

        public async Task<User> Login(string name, string password)
        {
            string authString = name + ":" + password;
            _authHeader = "Basic " + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(authString));

            var url = "/users/login";

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("Authorization", _authHeader);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = _client.GetAsync(url).Result;

            _user = null;

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                _user = JsonConvert.DeserializeObject<User>(content);
            }

            return _user;
        }

        public string GetHeader()
        {
            return _authHeader;
        }

        public User GetUser()
        {
            return _user;
        }
    }
}