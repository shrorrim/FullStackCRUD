using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using X4GA1C_HFT_2023241.Models;

namespace X4GA1C_HFT_2023241.Repository
{
    public class LaptopWebShopDbContext : DbContext
    {

        // data sets:

        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Laptop> Laptops { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Orderer> Orderers { get; set; }


        public LaptopWebShopDbContext()
        {
            this.Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (builder.IsConfigured == false)
            {
                //string connectionStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\LocalDataBase.mdf;Integrated Security=True;MultipleActiveResultSets=true";
                
                builder.UseLazyLoadingProxies()
                       .UseInMemoryDatabase("webshopDatabase");
            }
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            // laptop table connected with Brand table
            // laptop - brand
            // ( N --->   1  )
            // More to one connection
            builder.Entity<Laptop>(entity =>
            {
                entity.HasOne(laptop => laptop.Brand) // laptop has one Brand navigation property
                 .WithMany(brand => brand.Laptops) // brand contains multiple cars
                 .HasForeignKey(laptop => laptop.BrandId)
                 .OnDelete(DeleteBehavior.Cascade);

            });


            // laptop table connected with Orderer table trough Order
            // ( N --->   N )

            builder.Entity<Orderer>()
                .HasMany(orderer => orderer.OrderedLaptops) // orderer may order more
                .WithMany(laptop => laptop.Orderers) // one laptop sold to many 
                .UsingEntity<Order>(
                x => x.HasOne(x => x.Laptop)
                .WithMany().HasForeignKey(x => x.LaptopId).OnDelete(DeleteBehavior.Cascade),
                x => x.HasOne(x => x.Orderer)
                .WithMany().HasForeignKey(x => x.OrdererId).OnDelete(DeleteBehavior.Cascade));

            //we want to see the connecting table too... (Order table) beacuse of the information about the order

            // 1 to many connection, like :  orderer --> order
            builder.Entity<Order>()
                .HasOne(order => order.Orderer)
                .WithMany()
                .HasForeignKey(x => x.OrdererId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Order>()
                .HasOne(laptop => laptop.Laptop)
                .WithMany()
                .HasForeignKey(x => x.LaptopId)
                .OnDelete(DeleteBehavior.Cascade);


            Brand apple = new Brand() {Id = 1, Name = "Apple", YearOfAppearance = 1976};
            Brand asus = new Brand() {Id = 2, Name = "Asus", YearOfAppearance = 1989};
            Brand hp = new Brand() {Id = 3, Name = "Hp", YearOfAppearance = 1939};
            Brand dell = new Brand() {Id = 4, Name = "Dell", YearOfAppearance = 1984};
            Brand acer = new Brand() {Id = 5, Name = "Acer", YearOfAppearance = 1976};
            Brand lenovo = new Brand() {Id = 6, Name = "Lenovo", YearOfAppearance = 1984};
            Brand msi = new Brand() {Id = 7, Name = "Msi", YearOfAppearance = 1986};
            Brand razer = new Brand() {Id = 8, Name = "Razer", YearOfAppearance = 2005};

            var brands = new List<Brand>{ apple, asus, hp, dell, acer , lenovo , msi, razer };

            Laptop a1 = new Laptop() { Id = 1, ModelName = "VivoBook" , Processor = "Intel Celeron", RAM = 4 , Storage = 128 , RAM_Upgradeable = false , BrandId = asus.Id , Price = 200000};
            Laptop a2 = new Laptop() { Id = 2, ModelName = "VivoBook X" , Processor = "Intel Core i5", RAM = 8 , Storage = 512 , RAM_Upgradeable = false , BrandId = asus.Id, Price = 300000 };
            Laptop a3 = new Laptop() { Id = 3, ModelName = "VivoBook S14" , Processor = "Intel Core i7", RAM = 8 , Storage = 256 , RAM_Upgradeable = false , BrandId = asus.Id, Price = 459999 };
            Laptop a4 = new Laptop() { Id = 4, ModelName = "ROG Zephyrus M16", Processor = "Intel Core i7", RAM = 16 , Storage = 1000 , RAM_Upgradeable = true , BrandId = asus.Id, Price = 756000 };
            Laptop a5 = new Laptop() { Id = 5, ModelName = "ZenBook Pro 15 Flip" , Processor = "Intel Core i7", RAM = 16 , Storage = 512 , RAM_Upgradeable = false , BrandId = asus.Id, Price = 580000 };
            Laptop app1 = new Laptop() { Id = 6, ModelName = "Macbook Pro 16", Processor = "M2 Pro", RAM = 32, Storage = 2000, RAM_Upgradeable = false, BrandId = apple.Id, Price = 990000 };
            Laptop app2 = new Laptop() { Id = 7, ModelName = "Macbook Pro 16", Processor = "M2 Max", RAM = 64, Storage = 4000, RAM_Upgradeable = false, BrandId = apple.Id, Price = 1525000 };
            Laptop app3 = new Laptop() { Id = 8, ModelName = "Macbook Pro 13", Processor = "M1", RAM = 32, Storage = 1000, RAM_Upgradeable = false, BrandId = apple.Id, Price = 512000 };
            Laptop hp1 = new Laptop() { Id = 9, ModelName = "255 G9", Processor = "AMD Ryzen 5", RAM = 16, Storage = 512, RAM_Upgradeable = false, BrandId = hp.Id, Price = 250000 };
            Laptop hp2 = new Laptop() { Id = 10, ModelName = "Victus 16", Processor = "AMD Ryzen 5", RAM = 32, Storage = 1000, RAM_Upgradeable = true, BrandId = hp.Id, Price = 550000 };
            Laptop d1 = new Laptop() { Id = 11, ModelName = "Precision 7680", Processor = "Intel Core i9", RAM = 64, Storage = 4000, RAM_Upgradeable = true, BrandId = dell.Id, Price = 999999 };
            Laptop ac1 = new Laptop() { Id = 12, ModelName = "Swift Go", Processor = "Intel Core i5", RAM = 16, Storage = 512, RAM_Upgradeable = false, BrandId = acer.Id, Price = 420000 };
            Laptop l1 = new Laptop() { Id = 13, ModelName = "ThinkPad X1 Extreme", Processor = "Intel Core i7", RAM = 32, Storage = 512, RAM_Upgradeable = true, BrandId = lenovo.Id, Price = 1200000 };
            Laptop m1 = new Laptop() { Id = 14, ModelName = "Katana 17", Processor = "Intel Core i7", RAM = 16, Storage = 512, RAM_Upgradeable = true, BrandId = msi.Id, Price = 600000 };
            Laptop r1 = new Laptop() { Id = 15, ModelName = "Blade 17", Processor = "Intel Core i9", RAM = 32, Storage = 1000, RAM_Upgradeable = true, BrandId = razer.Id, Price = 1750000 };

            var laptops = new List<Laptop> { a1,a2,a3,a4,a5,app1,app2,app3,hp1,hp2,d1,ac1,l1,m1,r1 };

            Orderer o1 = new Orderer() {Id=1,Name = "Aaron", PhoneNumber="06704523412" };
            Orderer o2 = new Orderer() {Id=2,Name = "Truman", PhoneNumber="06304823112" };
            Orderer o3 = new Orderer() {Id=3,Name = "Max", PhoneNumber="062001223112" };
            Orderer o4 = new Orderer() {Id=4,Name = "Armstrong", PhoneNumber="06208293112" };
            Orderer o5 = new Orderer() {Id=5,Name = "Bill", PhoneNumber="067004823444" };
            Orderer o6 = new Orderer() {Id=6,Name = "David", PhoneNumber="06304823342" };
            Orderer o7 = new Orderer() {Id=7,Name = "Dexter", PhoneNumber="06204828612" };

            var orderers = new List<Orderer>() { o1,o2,o3,o4,o5,o6,o7};


            // o1 ordered 2 laptops on the same day (a1 and a2):
            Order order1 = new Order() { Id = 1, Date = DateTime.Parse("2023.02.20"), LaptopId = a1.Id, OrdererId = o1.Id };
            Order order2 = new Order() { Id = 2, Date = DateTime.Parse("2023.02.20"), LaptopId = a2.Id, OrdererId = o1.Id };

            Order order3 = new Order() { Id = 3, Date = DateTime.Parse("2023.06.12"), LaptopId = hp1.Id, OrdererId = o2.Id };
            Order order4 = new Order() { Id = 4, Date = DateTime.Parse("2023.09.23"), LaptopId = app2.Id, OrdererId = o3.Id };

            Order order5 = new Order() { Id = 5, Date = DateTime.Parse("2023.10.04"), LaptopId = r1.Id, OrdererId = o4.Id };
            Order order6 = new Order() { Id = 6, Date = DateTime.Parse("2023.11.06"), LaptopId = a4.Id, OrdererId = o2.Id };
            Order order7 = new Order() { Id = 7, Date = DateTime.Parse("2023.12.11"), LaptopId = app3.Id, OrdererId = o7.Id };




            builder.Entity<Brand>().HasData(brands);
            builder.Entity<Laptop>().HasData(laptops);
            builder.Entity<Orderer>().HasData(orderers);
            builder.Entity<Order>().HasData(order1,order2,order3,order4,order5,order6,order7);


        }

    }
}
