using System.Net.Http;

namespace SmartHouseLights.Services.Interfaces
{
    public interface IConnectionFactory
    {
        HttpClient GetHttpClient();
        void SetAuthenticationHeader(string authHeader);

    }
}