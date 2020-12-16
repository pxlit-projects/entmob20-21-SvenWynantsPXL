namespace SmartHouseLights.Models
{
    public class Light
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Brightness { get; set; }
        public bool OnState { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public LightGroup Group { get; set; }
    }
}