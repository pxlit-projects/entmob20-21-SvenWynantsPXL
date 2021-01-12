using Microsoft.EntityFrameworkCore;
using SmartHouseLights.Domain.Models;

namespace SmartHouseLights.Data
{
    public class SmartHouseContext : DbContext
    {
        public DbSet<Light> Lights { get; set; }
        public DbSet<UserLightStatistic> UserLightStatistics { get; set; }
        public SmartHouseContext() { }

        public SmartHouseContext(DbContextOptions<SmartHouseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data source = smarthouse.db");
            }
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Light>().HasKey(l => l.Id);
            modelBuilder.Entity<UserLightStatistic>().HasKey(u => u.Id);

            modelBuilder.Entity<UserLightStatistic>().HasOne<Light>(u => u.Light).WithMany(s => s.Statistics).HasForeignKey(u => u.LightId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
