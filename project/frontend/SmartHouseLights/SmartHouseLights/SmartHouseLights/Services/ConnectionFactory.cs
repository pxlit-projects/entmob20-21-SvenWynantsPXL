using System;
using System.Net.Http;
using SmartHouseLights.Services.Interfaces;

namespace SmartHouseLights.Services
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string _baseUrl = "http://192.168.51.228:8080";
        private static string _authHeader = "";

        public HttpClient GetHttpClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl)
            };

            if (!_authHeader.Equals(""))
            {
                client.DefaultRequestHeaders.Add("Authorization", _authHeader);
            }

            return client;
        }

        public void SetAuthenticationHeader(string authHeader)
        {
            _authHeader = authHeader;
        }
    }
}