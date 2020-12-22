using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using SmartHouseLights.Models;
using SmartHouseLights.Services.Interfaces;

namespace SmartHouseLights.Services
{
    public class HttpService : IHttpService
    {
        private readonly IAuthenticationService _authService;
        private readonly HttpClient _client;
        private string _baseUrl = "http://192.168.51.228:8080";

        public HttpService(IAuthenticationService authService)
        {
            _authService = authService;
            _client = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl)
            };
        }

        public List<Light> GetAllLights()
        {
            string authHeader = _authService.GetHeader();
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