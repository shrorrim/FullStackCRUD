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
                .WithMany(orderer => orderer.Orders)
                .HasForeignKey(x => x.OrdererId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Order>()
                .HasOne(laptop => laptop.Laptop)
                .WithMany(laptop => laptop.Orders)
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

            Laptop l2 = new Laptop() { Id = 16, ModelName = "IdeaPad 82V7008LHV", Processor = "Intel Celeron", RAM = 4, Storage = 128, RAM_Upgradeable = false, BrandId = lenovo.Id, Price = 69900 };
            Laptop l3 = new Laptop() { Id = 17, ModelName = "IdeaPad 3 82H803QEHV", Processor = "Intel Core i5", RAM = 16, Storage = 512, RAM_Upgradeable = false, BrandId = lenovo.Id, Price = 220000 };
            Laptop l4 = new Laptop() { Id = 18, ModelName = "ThinkPad E16 21JT000DHV", Processor = "AMD Ryzen 5", RAM = 16, Storage = 512, RAM_Upgradeable = false, BrandId = lenovo.Id, Price = 279000 };

            Laptop d2 = new Laptop() { Id = 19, ModelName = "Inspiron 3511", Processor = "Intel Core i3", RAM = 8, Storage = 256, RAM_Upgradeable = true, BrandId = dell.Id, Price = 165000 };
            Laptop d3 = new Laptop() { Id = 20, ModelName = "Inspiron 3520", Processor = "Intel Core i5", RAM = 8, Storage = 512, RAM_Upgradeable = true, BrandId = dell.Id, Price = 185000 };

            Laptop ac2 = new Laptop() { Id = 21, ModelName = "Aspire 3", Processor = "Intel Core i5", RAM = 8, Storage = 512, RAM_Upgradeable = false, BrandId = acer.Id, Price = 239000 };
            Laptop ac3 = new Laptop() { Id = 22, ModelName = "Extensa", Processor = "Intel Celeron", RAM = 4, Storage = 256, RAM_Upgradeable = false, BrandId = acer.Id, Price = 89900 };

            Laptop hp3 = new Laptop() { Id = 23, ModelName = "ProBook 450 G10", Processor = "Intel Core i5", RAM = 8, Storage = 512, RAM_Upgradeable = true, BrandId = hp.Id, Price = 398900 };

            Laptop a6 = new Laptop() { Id = 24, ModelName = "ProArt StudioBook Pro 16", Processor = "Intel Xeon", RAM = 64, Storage = 4000, RAM_Upgradeable = true, BrandId = asus.Id, Price = 2549900 };
            Laptop a7 = new Laptop() { Id = 25, ModelName = "ExpertBook P1", Processor = "Intel Core i3", RAM = 4, Storage = 256, RAM_Upgradeable = true, BrandId = asus.Id, Price = 129000 };

            var laptops = new List<Laptop> { a1,a2,a3,a4,a5,app1,app2,app3,hp1,hp2,d1,ac1,l1,m1,r1, l2,l3,l4,d2,d3,ac2,ac3,hp3,a6,a7 };

            Orderer o1 = new Orderer() {Id=1,Name = "Aaron Finch", PhoneNumber="06704523412" };
            Orderer o2 = new Orderer() {Id=2,Name = "Wolf Tammuz", PhoneNumber="06304823112" };
            Orderer o3 = new Orderer() {Id=3,Name = "Paschalis Nanda", PhoneNumber="062001223112" };
            Orderer o4 = new Orderer() {Id=4,Name = "Linda Berenice", PhoneNumber="06208293112" };
            Orderer o5 = new Orderer() {Id=5,Name = "Blanka Kajuun", PhoneNumber="067004823444" };
            Orderer o6 = new Orderer() {Id=6,Name = "Renata Roza", PhoneNumber="06304823342" };
            Orderer o7 = new Orderer() {Id=7,Name = "Tatjana Caylee", PhoneNumber="06204828612" };

            Orderer o8 = new Orderer() { Id = 8, Name = "Limbikani Cahaya", PhoneNumber="06706927456" };
            Orderer o9 = new Orderer() { Id = 9, Name = "Donnie Darko", PhoneNumber="06302126539" };
            Orderer o10 = new Orderer() { Id = 10, Name = "Khristofor Austin", PhoneNumber="06201983478" };
            Orderer o11 = new Orderer() { Id = 11, Name = "Troilus Reed", PhoneNumber="06209835613" };
            Orderer o12 = new Orderer() { Id = 12, Name = "Vasiliy Novodany", PhoneNumber="06703010238" };
            Orderer o13 = new Orderer() { Id = 13, Name = "Maximilian Estantra", PhoneNumber="06308912303" };
            Orderer o14 = new Orderer() { Id = 14, Name = "Odo Jong-Su", PhoneNumber="06709036688" };
            Orderer o15 = new Orderer() { Id = 15, Name = "Mario Ruprecht", PhoneNumber="06204513299" };
            Orderer o16 = new Orderer() { Id = 16, Name = "Misho Honorinus", PhoneNumber="06704059678" };

            var orderers = new List<Orderer>() { o1,o2,o3,o4,o5,o6,o7, o8, o9, o10, o11,o12,o13,o14,o15,o16 };


            // o1 ordered 2 laptops on the same day (a1 and a2):
            Order order1 = new Order() { Id = 1, Date = DateTime.Parse("2023.02.20"), LaptopId = a1.Id, OrdererId = o1.Id };
            Order order2 = new Order() { Id = 2, Date = DateTime.Parse("2023.02.20"), LaptopId = a2.Id, OrdererId = o1.Id };

            Order order3 = new Order() { Id = 3, Date = DateTime.Parse("2023.06.12"), LaptopId = hp1.Id, OrdererId = o2.Id };
            Order order4 = new Order() { Id = 4, Date = DateTime.Parse("2023.09.23"), LaptopId = app2.Id, OrdererId = o3.Id };

            Order order5 = new Order() { Id = 5, Date = DateTime.Parse("2023.10.04"), LaptopId = r1.Id, OrdererId = o4.Id };
            Order order6 = new Order() { Id = 6, Date = DateTime.Parse("2023.11.06"), LaptopId = a4.Id, OrdererId = o2.Id };
            Order order7 = new Order() { Id = 7, Date = DateTime.Parse("2023.12.11"), LaptopId = app3.Id, OrdererId = o7.Id };

            Order order8 = new Order() { Id = 8, Date = DateTime.Parse("2023.12.11"), LaptopId = d3.Id, OrdererId = o12.Id };
            Order order9 = new Order() { Id = 9, Date = DateTime.Parse("2023.12.11"), LaptopId = l4.Id, OrdererId = o12.Id };

            Order order10 = new Order() { Id = 10, Date = DateTime.Parse("2023.09.26"), LaptopId = d3.Id, OrdererId = o8.Id };
            Order order11 = new Order() { Id = 11, Date = DateTime.Parse("2023.09.26"), LaptopId = a6.Id, OrdererId = o8.Id };
            Order order12 = new Order() { Id = 12, Date = DateTime.Parse("2023.09.26"), LaptopId = ac1.Id, OrdererId = o8.Id };

            Order order13 = new Order() { Id = 13, Date = DateTime.Parse("2023.05.20"), LaptopId = hp1.Id, OrdererId = o9.Id };
            Order order14 = new Order() { Id = 14, Date = DateTime.Parse("2023.08.22"), LaptopId = hp2.Id, OrdererId = o10.Id };
            Order order15 = new Order() { Id = 15, Date = DateTime.Parse("2023.06.03"), LaptopId = hp3.Id, OrdererId = o11.Id };
            Order order16 = new Order() { Id = 16, Date = DateTime.Parse("2023.04.19"), LaptopId = l2.Id, OrdererId = o12.Id };

            Order order17 = new Order() { Id = 17, Date = DateTime.Parse("2023.10.22"), LaptopId = r1.Id, OrdererId = o8.Id };

            var orders = new List<Order>() { order1, order2, order3, order4, order5, order6, order7, order8,
                order9, order10, order11, order12, order13, order14, order15, order16,order17 };

            builder.Entity<Brand>().HasData(brands);
            builder.Entity<Laptop>().HasData(laptops);
            builder.Entity<Orderer>().HasData(orderers);
            builder.Entity<Order>().HasData(orders);


        }

    }
}
