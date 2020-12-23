﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using SmartHouseLights.Models;
using SmartHouseLights.Services.Interfaces;

namespace SmartHouseLights.Services
{
    public class LightService : ILightService
    {
        private readonly IAuthenticationService _authService;
        private readonly HttpClient _client;
        private readonly string _baseUrl = "http://192.168.1.19:8080";

        public LightService(IAuthenticationService authService)
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
            _client.DefaultRequestHeaders.Clear();
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

        public Light FlipSwitch(int id)
        {
            var url = $"/lights/{id}/flipSwitch";
            string authHeader = _authService.GetHeader();

            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("Authorization", authHeader);
            HttpResponseMessage response = _client.PutAsync(url, null).Result;
            Light light = null;

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                light = JsonConvert.DeserializeObject<Light>(content);
            }

            return light;
        }
    }
}