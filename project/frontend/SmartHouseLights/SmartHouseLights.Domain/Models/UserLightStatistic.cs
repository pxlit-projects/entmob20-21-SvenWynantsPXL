using System;

namespace SmartHouseLights.Domain.Models
{
    public class UserLightStatistic
    {
        public int Id { get; set; }
        public int LightId { get; set; }
        public Light Light { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime TurnedOnTime { get; set; }
        public double HoursOn { get; set; }
    }
}