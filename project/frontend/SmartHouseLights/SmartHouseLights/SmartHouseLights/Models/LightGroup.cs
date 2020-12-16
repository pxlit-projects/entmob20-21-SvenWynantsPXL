using System.Collections.Generic;

namespace SmartHouseLights.Models
{
    public class LightGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasOnState { get; set; }
        public bool AllOnState { get; set; }
        public int Brightness { get; set; }
        public List<Light> Lights { get; set; }
    }
}