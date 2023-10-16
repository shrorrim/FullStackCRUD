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


        protected override OnModelCrating(ModelBuilder builder)
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
                 .OnDelete(DeleteBehavior.ClientSetNull);

            });


            // laptop table connected with Order table
            // laptop - order
            // ( N --->   1  )
            // More to one connection
            builder.Entity<Laptop>(entity =>
            {
                entity.HasOne(laptop => laptop.Order) // laptop has order navigation property
                .WithMany(order => order.Laptops) // one order may contains more notebooks
                .HasForeignKey(laptop => laptop.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            });


            // fill up the database with our datas:


            Brand apple = new Brand() {Id = 1, Name = "Apple", YearOfAppearance = 1976};
            Brand asus = new Brand() {Id = 2, Name = "Asus", YearOfAppearance = 1989};
            Brand hp = new Brand() {Id = 3, Name = "Hp", YearOfAppearance = 1939};
            Brand dell = new Brand() {Id = 4, Name = "Dell", YearOfAppearance = 1984};
            Brand acer = new Brand() {Id = 5, Name = "Acer", YearOfAppearance = 1976};
            Brand lenovo = new Brand() {Id = 6, Name = "Lenovo", YearOfAppearance = 1984};
            Brand msi = new Brand() {Id = 7, Name = "Msi", YearOfAppearance = 1986};
            Brand razer = new Brand() {Id = 8, Name = "Razer", YearOfAppearance = 2005};



            Laptop a1 = new Laptop() { Id = 1, ModelName = "VivoBook" , Processor = "Intel Celeron", RAM = 4 , Storage = 128 , RAM_Upgradeable = false , BrandId = asus.Id };
            Laptop a2 = new Laptop() { Id = 2, ModelName = "VivoBook X" , Processor = "Intel Core i5", RAM = 8 , Storage = 512 , RAM_Upgradeable = false , BrandId = asus.Id };
            Laptop a3 = new Laptop() { Id = 3, ModelName = "VivoBook S14" , Processor = "Intel Core i7", RAM = 8 , Storage = 256 , RAM_Upgradeable = false , BrandId = asus.Id };
            Laptop a4 = new Laptop() { Id = 4, ModelName = "ROG Zephyrus M16", Processor = "Intel Core i7", RAM = 16 , Storage = 1000 , RAM_Upgradeable = true , BrandId = asus.Id };
            Laptop a5 = new Laptop() { Id = 5, ModelName = "ZenBook Pro 15 Flip" , Processor = "Intel Core i7", RAM = 16 , Storage = 512 , RAM_Upgradeable = false , BrandId = asus.Id };


            Laptop app1 = new Laptop() { Id = 6, ModelName = "Macbook Pro 16", Processor = "M2 Pro", RAM = 32, Storage = 2000, RAM_Upgradeable = false, BrandId = apple.Id };
            Laptop app2 = new Laptop() { Id = 7, ModelName = "Macbook Pro 16", Processor = "M2 Max", RAM = 64, Storage = 4000, RAM_Upgradeable = false, BrandId = apple.Id };
            Laptop app3 = new Laptop() { Id = 8, ModelName = "Macbook Pro 13", Processor = "M1", RAM = 32, Storage = 1000, RAM_Upgradeable = false, BrandId = apple.Id };


            Laptop hp1 = new Laptop() { Id = 9, ModelName = "255 G9", Processor = "AMD Ryzen 5", RAM = 16, Storage = 512, RAM_Upgradeable = false, BrandId = hp.Id };
            Laptop hp2 = new Laptop() { Id = 10, ModelName = "Victus 16", Processor = "AMD Ryzen 5", RAM = 32, Storage = 1000, RAM_Upgradeable = true, BrandId = hp.Id };

            Laptop d1 = new Laptop() { Id = 11, ModelName = "Precision 7680", Processor = "Intel Core i9", RAM = 64, Storage = 4000, RAM_Upgradeable = true, BrandId = dell.Id };

            Laptop ac1 = new Laptop() { Id = 12, ModelName = "Swift Go", Processor = "Intel Core i5", RAM = 16, Storage = 512, RAM_Upgradeable = false, BrandId = acer.Id };

            Laptop l1 = new Laptop() { Id = 13, ModelName = "ThinkPad X1 Extreme", Processor = "Intel Core i7", RAM = 32, Storage = 512, RAM_Upgradeable = true, BrandId = lenovo.Id };


            Laptop m1 = new Laptop() { Id = 14, ModelName = "Katana 17", Processor = "Intel Core i7", RAM = 16, Storage = 512, RAM_Upgradeable = true, BrandId = msi.Id };


            Laptop r1 = new Laptop() { Id = 15, ModelName = "Blade 17", Processor = "Intel Core i9", RAM = 32, Storage = 1000, RAM_Upgradeable = true, BrandId = razer.Id };


            Order order1 = new Order() { Id = 1,  NameOfTheOrderer = "Aaron",TimeOfTheOrder = new DateTime(2023,10,11) , Quantity = 1 };
            Order order2 = new Order() { Id = 2,  NameOfTheOrderer = "Truman",TimeOfTheOrder = new DateTime(2023,10,14) , Quantity = 1 };
            Order order3 = new Order() { Id = 3,  NameOfTheOrderer = "Max",TimeOfTheOrder = new DateTime(2023,10,15) , Quantity = 1 };
            Order order4 = new Order() { Id = 4,  NameOfTheOrderer = "Armstrong",TimeOfTheOrder = new DateTime(2023,10,15) , Quantity = 1 };
            Order order5 = new Order() { Id = 5,  NameOfTheOrderer = "Bill",TimeOfTheOrder = new DateTime(2023,10,15) , Quantity = 1 };
            Order order6 = new Order() { Id = 6,  NameOfTheOrderer = "David",TimeOfTheOrder = new DateTime(2023,10,20) , Quantity = 1 };
            Order order7 = new Order() { Id = 7,  NameOfTheOrderer = "Karen",TimeOfTheOrder = new DateTime(2023,10,20) , Quantity = 1 };





            builder.Entity<Brand>().HasData(apple, asus, acer,hp,dell,msi,razer,lenovo);
            builder.Entity<Laptop>().HasData(a1,a2,a3,a4,a5, app1, app2, app3, hp1, hp2, d1, ac1, l1, m1, r1);
            builder.Entity<Order>().HasData(order1,order2,order3,order4,order5,order6,order7);
        }

    }
}
