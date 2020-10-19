using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using StarWarsUniverse.Domain;

namespace StarWarsUniverse.Data.Repositories.Api
{
    public class PlanetApiRepository : IPlanetRepository
    {
        private readonly HttpClient _httpClient;

        public PlanetApiRepository()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://swapi.dev")
            };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public IList<Planet> GetAllPlanets()
        {
            var url = "/api/planets/";
            var allPlanets = new List<Planet>();
            ResultsPage<Planet> resultsPage = null;

            HttpResponseMessage response = _httpClient.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                resultsPage = JsonConvert.DeserializeObject<ResultsPage<Planet>>(content);
                allPlanets = resultsPage.Results;

                while (resultsPage.Next != null)
                {
                    url = resultsPage.Next;
                    response = _httpClient.GetAsync(url).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        content = response.Content.ReadAsStringAsync().Result;
                        resultsPage = JsonConvert.DeserializeObject<ResultsPage<Planet>>(content);
                        allPlanets.AddRange(resultsPage.Results);
                    }
                }
            }

            return allPlanets;
        }
    }
}