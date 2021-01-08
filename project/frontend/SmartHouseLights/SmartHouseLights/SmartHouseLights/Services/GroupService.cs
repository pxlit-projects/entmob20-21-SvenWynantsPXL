using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using SmartHouseLights.Models;
using SmartHouseLights.Services.Interfaces;

namespace SmartHouseLights.Services
{
    public class GroupService : IGroupService
    {
        private readonly HttpClient _client;
        public GroupService(IConnectionFactory connectionFactory)
        {
            _client = connectionFactory.GetHttpClient();
        }

        public List<LightGroup> GetAllGroups()
        {
            var url = "/groups/groups";

            HttpResponseMessage response = _client.GetAsync(url).Result;

            List<LightGroup> groups = new List<LightGroup>();

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                groups = JsonConvert.DeserializeObject<List<LightGroup>>(content);
            }

            return groups;
        }

        public LightGroup GetGroupById(int id)
        {
            var url = $"/groups/{id}";

            HttpResponseMessage response = _client.GetAsync(url).Result;

            LightGroup group = null;

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                group = JsonConvert.DeserializeObject<LightGroup>(content);
            }

            return group;
        }

        public LightGroup TurnAllLightsOnInGroup(int groupId)
        {
            var url = $"/groups/{groupId}/turnAllOn";

            HttpResponseMessage response = _client.PutAsync(url, null).Result;

            LightGroup group = null;

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                group = JsonConvert.DeserializeObject<LightGroup>(content);
            }

            return group;
        }

        public LightGroup TurnAllLightsOffInGroup(int groupId)
        {
            var url = $"/groups/{groupId}/turnAllOff";

            HttpResponseMessage response = _client.PutAsync(url, null).Result;

            LightGroup group = null;

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                group = JsonConvert.DeserializeObject<LightGroup>(content);
            }

            return group;
        }

        public LightGroup AddGroup(CreateGroupModel model)
        {
            var url = "groups/addGroup";
            string stringContent = JsonConvert.SerializeObject(model);
            HttpContent httpContent = new StringContent(stringContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _client.PostAsync(url, httpContent).Result;

            LightGroup group = null;

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                group = JsonConvert.DeserializeObject<LightGroup>(content);
            }

            return group;
        }
    }
}