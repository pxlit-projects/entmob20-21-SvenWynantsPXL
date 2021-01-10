using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace SmartHouseLights.Data
{
    public class SmartHouseContextFactory
    {
        public static string ConnectionString { get; set; }

        public static SmartHouseContext Create()
        {
            var options = new DbContextOptionsBuilder<SmartHouseContext>()
                .UseSqlite(ConnectionString)
                .Options;
            return new SmartHouseContext(options);
        }
    }
}
