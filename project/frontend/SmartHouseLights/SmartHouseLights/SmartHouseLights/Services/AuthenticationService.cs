using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartHouseLights.Models;

namespace SmartHouseLights.Services
{
    public class AuthenticationService
    {
        private string authString;
        private string baseUrl = "http://192.168.51.228:8080";
        private HttpClient _client;
        private string authHeader;
        public static User User;

        public AuthenticationService()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
        }

        public async Task<User> Login(string name, string password)
        {
            authString = name + ":" + password;
            authHeader = "Basic " + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(authString));

            var url = "/users/login";

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("Authorization", authHeader);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = _client.GetAsync(url).Result;

            User = null;

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                User = JsonConvert.DeserializeObject<User>(content);
            }

            return User;
        }
    }
}