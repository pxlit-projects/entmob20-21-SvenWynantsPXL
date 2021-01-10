using System.Collections.Generic;

namespace SmartHouseLights.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public List<LightGroup> Groups { get; set; }
    }
}