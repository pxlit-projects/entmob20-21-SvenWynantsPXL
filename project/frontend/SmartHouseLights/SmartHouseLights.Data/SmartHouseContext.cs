using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SmartHouseLights.Domain.Models;

namespace SmartHouseLights.Data
{
    public class SmartHouseContext : DbContext
    {
        public DbSet<UserLightStatistic> UserLightStatistics { get; set; }
        public SmartHouseContext() { }
        public SmartHouseContext(DbContextOptions<SmartHouseContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data source = smarthouse.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
