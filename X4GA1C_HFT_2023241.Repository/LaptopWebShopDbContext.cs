using Microsoft.EntityFrameworkCore;
using System;
using X4GA1C_HFT_2023241.Models;

namespace X4GA1C_HFT_2023241.Repository
{
    public class LaptopWebShopDbContext : DbContext
    {

        // data sets:

        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Laptop> Laptops { get; set; }
        public virtual DbSet<Order> Orders { get; set; }


        public LaptopWebShopDbContext()
        {
            this.Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (builder.IsConfigured == false)
            {
                // most még localdb vel de majd ---> in memory database!
                string connectionStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\LocalDataBase.mdf;Integrated Security=True";
                builder.UseSqlServer(connectionStr)
                    .UseLazyLoadingProxies();
            }
        }

    }
}
