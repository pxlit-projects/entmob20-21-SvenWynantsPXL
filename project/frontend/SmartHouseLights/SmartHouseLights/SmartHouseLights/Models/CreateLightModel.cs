namespace SmartHouseLights.Models
{
    public class CreateLightModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public Manufacturer LightManufacturer { get; set; }
        public int LightGroupId { get; set; }
    }
}