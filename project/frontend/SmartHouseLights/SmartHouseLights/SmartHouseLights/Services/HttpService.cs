using System;
using System.Collections.Generic;
using System.Net.Http;
using SmartHouseLights.Models;

namespace SmartHouseLights.Services
{
    public class HttpService
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

        public List<Light> getAllLights()
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            return null;
        }
    }
}