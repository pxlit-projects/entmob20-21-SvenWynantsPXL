namespace SmartHouseLights.Models
{
    public class Light
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Brightness { get; set; }
        public bool IsOn { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public int GroupId { get; set; }
    }
}