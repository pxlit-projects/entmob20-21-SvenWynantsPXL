using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using SmartHouseLights.Domain.Models;
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

        public Light AddLight(CreateLightModel createLight)
        {
            var url = "/lights/light";
            var stringContent = JsonConvert.SerializeObject(createLight);
            HttpContent httpContent = new StringContent(stringContent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync(url, httpContent).Result;

            Light light = null;

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                light = JsonConvert.DeserializeObject<Light>(content);
            }

            return light;
        }

        public Light UpdateLight(Light light)
        {
            var url = "/lights/updateLight";

            var stringContent = JsonConvert.SerializeObject(light);
            HttpContent httpContent = new StringContent(stringContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _client.PutAsync(url, httpContent).Result;

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                light = JsonConvert.DeserializeObject<Light>(content);
            }

            return light;
        }

        public Light GetLightById(int lightId)
        {
            var url = $"/lights/light/{lightId}";

            HttpResponseMessage response = _client.GetAsync(url).Result;

            Light light = null;

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                light = JsonConvert.DeserializeObject<Light>(content);
            }

            return light;
        }

        public bool DeleteLightById(int lightId)
        {
            var url = $"/lights/light/{lightId}";

            HttpResponseMessage response = _client.DeleteAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}