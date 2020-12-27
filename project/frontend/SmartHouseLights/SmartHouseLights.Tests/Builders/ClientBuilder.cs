using System;
using System.Net.Http;

namespace SmartHouseLights.Tests.Builders
{
    public class ClientBuilder
    {
        private readonly HttpClient _client;

        public ClientBuilder()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:8080")
            };
            string header = "Basic " + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("sven:pxl"));
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("Authorization", header);
        }

        public HttpClient Build()
        {
            return _client;
        }
    }
}