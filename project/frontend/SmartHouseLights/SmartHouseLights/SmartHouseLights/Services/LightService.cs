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
        private readonly HttpClient _client;

        public LightService(IConnectionFactory connectionFactory)
        {
            _client = connectionFactory.GetHttpClient();
        }

        public List<Light> GetAllLights()
        {
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
            
            HttpResponseMessage response = _client.PutAsync(url, null).Result;
            Light light = null;

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                light = JsonConvert.DeserializeObject<Light>(content);
            }

            return light;
        }

        public Light AddLight(Light light)
        {
            var url = "/lights/light";
            var stringContent = JsonConvert.SerializeObject(light);
            HttpContent httpContent = new StringContent(stringContent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync(url, httpContent).Result;

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                light = JsonConvert.DeserializeObject<Light>(content);
            }

            return light;
        }
    }
}