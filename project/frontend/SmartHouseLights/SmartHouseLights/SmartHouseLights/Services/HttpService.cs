using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using SmartHouseLights.Models;

namespace SmartHouseLights.Services
{
    public class HttpService : IHttpService
    {
        private HttpClient _client;
        private string _baseUrl = "http://192.168.51.228:8080";

        public HttpService()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl)
            };
        }

        public List<Light> GetAllLights(string name, string password)
        {
            string authString = name + ":" + password;
            string authHeader = "Basic " + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(authString));
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("Authorization", authHeader);
            var url = "/lights/lights";

            HttpResponseMessage response = _client.GetAsync(url).Result;

            List<Light> lights = new List<Light>();

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                lights = JsonConvert.DeserializeObject<List<Light>>(content);
            }

            return lights;
        }
    }
}