using Newtonsoft.Json;
using StarWarsUniverse.Domain;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace StarWarsUniverse.Data.Repositories.Api
{
    public abstract class ApiRepositoryBase
    {
        protected readonly HttpClient _httpClient;

        protected ApiRepositoryBase()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://swapi.dev") };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected IList<T> GetAllStarWarsResources<T>(string url) where T : Resource
        {
            var allResources = new List<T>();
            string currentUrl = url;

            do
            {
                HttpResponseMessage response = _httpClient.GetAsync(currentUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;
                    var resultsPage = JsonConvert.DeserializeObject<ResultsPage<T>>(content);
                    allResources.AddRange(resultsPage.Results);
                    currentUrl = resultsPage.Next;
                }
            } while (currentUrl != null);

            return allResources;
        }
    }
}
