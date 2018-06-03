using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WeighrDAL.Models;

namespace WeighrDAL
{
    public class WeighrContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<ShiftTarget> ShiftTargets { get; set; }
        public DbSet<ScaleConfig> ScaleConfigs { get; set; }
        public DbSet<AccumulatedWeight> AccumulatedWeights { get; set; }
        public DbSet<ScaleSetting> ScaleSettings { get; set; }
        public DbSet<TransactionLog> TransactionLogs { get; set; }

        public DbSet<Batch> Batches { get; set; }

        public DbSet<DeviceInfo> DeviceInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=weighr.db");
            ////Add scripts as project resources and use it like:
            //string sql = Resources._20170630085940_AddMigration;
            //migrationBuilder.Sql(sql);
        }
    }

   
}