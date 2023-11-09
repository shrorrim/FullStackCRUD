using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4GA1C_HFT_2023241.Logic;
using X4GA1C_HFT_2023241.Models;
using X4GA1C_HFT_2023241.Repository;
using X4GA1C_HFT_2023241.Repository.Repositories;

namespace X4GA1C_HFT_2023241.Test
{
    [TestFixture]
    public class OrderLogicTesterClass
    {
        OrderLogic orderLogic;
        Mock<IOrderRepository> mockOrderRepository;

        [SetUp]
        public void Init()
        {
            var mockBrandData = new List<Brand>()
            {
                 new Brand() {Id = 1, Name = "Apple", YearOfAppearance = 1976},
                 new Brand() { Id = 2, Name = "Asus", YearOfAppearance = 1989 },
                 new Brand() { Id = 3, Name = "Hp", YearOfAppearance = 1939 },
                 new Brand() { Id = 4, Name = "Dell", YearOfAppearance = 1984 },
                 new Brand() { Id = 5, Name = "Acer", YearOfAppearance = 1976 },
                 new Brand() { Id = 6, Name = "Lenovo", YearOfAppearance = 1984 },
                 new Brand() { Id = 7, Name = "Msi", YearOfAppearance = 1986 },
                 new Brand() { Id = 8, Name = "Razer", YearOfAppearance = 2005 }

            }.AsQueryable();

            var mockLaptopData = new List<Laptop>()
            {
               new Laptop() { Id = 1, ModelName = "VivoBook" , Processor = "Intel Celeron", RAM = 4 , Storage = 128 , RAM_Upgradeable = false , BrandId = 2 , Price = 200000},
               new Laptop() { Id = 2, ModelName = "VivoBook X", Processor = "Intel Core i5", RAM = 8, Storage = 512, RAM_Upgradeable = false, BrandId = 2, Price = 300000 },
               new Laptop() { Id = 3, ModelName = "VivoBook S14", Processor = "Intel Core i7", RAM = 8, Storage = 256, RAM_Upgradeable = false, BrandId = 2, Price = 459999 },
               new Laptop() { Id = 4, ModelName = "ROG Zephyrus M16", Processor = "Intel Core i7", RAM = 16, Storage = 1000, RAM_Upgradeable = true, BrandId = 2, Price = 756000 },
               new Laptop() { Id = 5, ModelName = "ZenBook Pro 15 Flip", Processor = "Intel Core i7", RAM = 16, Storage = 512, RAM_Upgradeable = false, BrandId = 2, Price = 580000 },
               new Laptop() { Id = 6, ModelName = "Macbook Pro 16", Processor = "M2 Pro", RAM = 32, Storage = 2000, RAM_Upgradeable = false, BrandId = 1, Price = 990000 },
               new Laptop() { Id = 7, ModelName = "Macbook Pro 16", Processor = "M2 Max", RAM = 64, Storage = 4000, RAM_Upgradeable = false, BrandId = 1, Price = 1525000 },
               new Laptop() { Id = 8, ModelName = "Macbook Pro 13", Processor = "M1", RAM = 32, Storage = 1000, RAM_Upgradeable = false, BrandId = 1, Price = 512000 },
               new Laptop() { Id = 9, ModelName = "255 G9", Processor = "AMD Ryzen 5", RAM = 16, Storage = 512, RAM_Upgradeable = false, BrandId = 3, Price = 250000 },
               new Laptop() { Id = 10, ModelName = "Victus 16", Processor = "AMD Ryzen 5", RAM = 32, Storage = 1000, RAM_Upgradeable = true, BrandId = 3, Price = 550000 },
               new Laptop() { Id = 11, ModelName = "Precision 7680", Processor = "Intel Core i9", RAM = 64, Storage = 4000, RAM_Upgradeable = true, BrandId = 4, Price = 999999 },
               new Laptop() { Id = 12, ModelName = "Swift Go", Processor = "Intel Core i5", RAM = 16, Storage = 512, RAM_Upgradeable = false, BrandId = 5, Price = 420000 },
               new Laptop() { Id = 13, ModelName = "ThinkPad X1 Extreme", Processor = "Intel Core i7", RAM = 32, Storage = 512, RAM_Upgradeable = true, BrandId = 6, Price = 1200000 },
               new Laptop() { Id = 14, ModelName = "Katana 17", Processor = "Intel Core i7", RAM = 16, Storage = 512, RAM_Upgradeable = true, BrandId = 7, Price = 600000 },
               new Laptop() { Id = 15, ModelName = "Blade 17", Processor = "Intel Core i9", RAM = 32, Storage = 1000, RAM_Upgradeable = true, BrandId = 8, Price = 1750000 },

               new Laptop() { Id = 16, ModelName = "IdeaPad 82V7008LHV", Processor = "Intel Celeron", RAM = 4, Storage = 128, RAM_Upgradeable = false, BrandId = 6, Price = 69900 },
               new Laptop() { Id = 17, ModelName = "IdeaPad 3 82H803QEHV", Processor = "Intel Core i5", RAM = 16, Storage = 512, RAM_Upgradeable = false, BrandId = 6, Price = 220000 },
               new Laptop() { Id = 18, ModelName = "ThinkPad E16 21JT000DHV", Processor = "AMD Ryzen 5", RAM = 16, Storage = 512, RAM_Upgradeable = false, BrandId = 6, Price = 279000 },

               new Laptop() { Id = 19, ModelName = "Inspiron 3511", Processor = "Intel Core i3", RAM = 8, Storage = 256, RAM_Upgradeable = true, BrandId = 4, Price = 165000 },
               new Laptop() { Id = 20, ModelName = "Inspiron 3520", Processor = "Intel Core i5", RAM = 8, Storage = 512, RAM_Upgradeable = true, BrandId = 4, Price = 185000 },

               new Laptop() { Id = 21, ModelName = "Aspire 3", Processor = "Intel Core i5", RAM = 8, Storage = 512, RAM_Upgradeable = false, BrandId = 5, Price = 239000 },
               new Laptop() { Id = 22, ModelName = "Extensa", Processor = "Intel Celeron", RAM = 4, Storage = 256, RAM_Upgradeable = false, BrandId = 5, Price = 89900 },

               new Laptop() { Id = 23, ModelName = "ProBook 450 G10", Processor = "Intel Core i5", RAM = 8, Storage = 512, RAM_Upgradeable = true, BrandId = 3, Price = 398900 },

               new Laptop() { Id = 24, ModelName = "ProArt StudioBook Pro 16", Processor = "Intel Xeon", RAM = 64, Storage = 4000, RAM_Upgradeable = true, BrandId = 2, Price = 2549900 },
               new Laptop() { Id = 25, ModelName = "ExpertBook P1", Processor = "Intel Core i3", RAM = 4, Storage = 256, RAM_Upgradeable = true, BrandId = 2, Price = 129000 }

            }.AsQueryable();

            var mockOrdererData = new List<Orderer>()
            {
                    new Orderer() {Id=1,Name = "Aaron Finch", PhoneNumber="06704523412" },
                    new Orderer() { Id = 2, Name = "Wolf Tammuz", PhoneNumber = "06304823112" },
                    new Orderer() { Id = 3, Name = "Paschalis Nanda", PhoneNumber = "062001223112" },
                    new Orderer() { Id = 4, Name = "Linda Berenice", PhoneNumber = "06208293112" },
                    new Orderer() { Id = 5, Name = "Blanka Kajuun", PhoneNumber = "067004823444" },
                    new Orderer() { Id = 6, Name = "Renata Roza", PhoneNumber = "06304823342" },
                    new Orderer() { Id = 7, Name = "Tatjana Caylee", PhoneNumber = "06204828612" },

                    new Orderer() { Id = 8, Name = "Limbikani Cahaya", PhoneNumber = "06706927456" },
                    new Orderer() { Id = 9, Name = "Donnie Darko", PhoneNumber = "06302126539" },
                    new Orderer() { Id = 10, Name = "Khristofor Austin", PhoneNumber = "06201983478" },
                    new Orderer() { Id = 11, Name = "Troilus Reed", PhoneNumber = "06209835613" },
                    new Orderer() { Id = 12, Name = "Vasiliy Novodany", PhoneNumber = "06703010238" },
                    new Orderer() { Id = 13, Name = "Maximilian Estantra", PhoneNumber = "06308912303" },
                    new Orderer() { Id = 14, Name = "Odo Jong-Su", PhoneNumber = "06709036688" },
                    new Orderer() { Id = 15, Name = "Mario Ruprecht", PhoneNumber = "06204513299" },
                    new Orderer() { Id = 16, Name = "Misho Honorinus", PhoneNumber = "06704059678" }
            }.AsQueryable();

            var mockOrderData = new List<Order>()
            {
                 new Order() { Id = 1, Date = DateTime.Parse("2023.02.20"), LaptopId = 1, OrdererId = 1 },
                 new Order() { Id = 2, Date = DateTime.Parse("2023.02.20"), LaptopId = 2, OrdererId =1 },

                 new Order() { Id = 3, Date = DateTime.Parse("2023.06.12"), LaptopId = 9, OrdererId = 2 },
                 new Order() { Id = 4, Date = DateTime.Parse("2023.09.23"), LaptopId = 7, OrdererId = 3 },

                 new Order() { Id = 5, Date = DateTime.Parse("2023.10.04"), LaptopId = 15, OrdererId = 4 },
                 new Order() { Id = 6, Date = DateTime.Parse("2023.11.06"), LaptopId = 4, OrdererId = 3 },
                 new Order() { Id = 7, Date = DateTime.Parse("2023.12.11"), LaptopId = 8, OrdererId = 7 },

                 new Order() { Id = 8, Date = DateTime.Parse("2023.12.11"), LaptopId = 20, OrdererId = 12 },
                 new Order() { Id = 9, Date = DateTime.Parse("2023.12.11"), LaptopId = 18, OrdererId = 12 },

                 new Order() { Id = 10, Date = DateTime.Parse("2023.09.26"), LaptopId = 20, OrdererId = 8 },
                 new Order() { Id = 11, Date = DateTime.Parse("2023.09.26"), LaptopId = 24, OrdererId = 8 },
                 new Order() { Id = 12, Date = DateTime.Parse("2023.09.26"), LaptopId = 12, OrdererId = 8 },

                 new Order() { Id = 13, Date = DateTime.Parse("2023.05.20"), LaptopId = 9, OrdererId = 9 },
                 new Order() { Id = 14, Date = DateTime.Parse("2023.08.22"), LaptopId = 10, OrdererId = 10 },
                 new Order() { Id = 15, Date = DateTime.Parse("2023.06.03"), LaptopId = 23, OrdererId = 11 },
                 new Order() { Id = 16, Date = DateTime.Parse("2023.04.19"), LaptopId = 16, OrdererId = 12 },

                 new Order() { Id = 17, Date = DateTime.Parse("2023.10.22"), LaptopId = 15, OrdererId = 8 }

            }.AsQueryable();

            // Create relationships between laptops and brands:
            foreach (var laptop in mockLaptopData)
            {
                laptop.Brand = mockBrandData.FirstOrDefault(b => b.Id == laptop.BrandId);
            }


            // Link relationships between orders, laptops, and orderers:
            foreach (var order in mockOrderData)
            {
                order.Laptop = mockLaptopData.FirstOrDefault(l => l.Id == order.LaptopId);
                order.Orderer = mockOrdererData.FirstOrDefault(o => o.Id == order.OrdererId);


                if (order.Laptop != null)
                {
                    // create the collection if its null:
                    if (order.Laptop.Orders == null)
                    {
                        order.Laptop.Orders = new List<Order>();
                    }

                    //Add the laptop the currect order:
                    order.Laptop.Orders.Add(order);
                }

                if (order.Orderer != null)
                {
                    //add ther current order to the orderer:
                    order.Orderer.Orders.Add(order);

                    // create the collection if its null:
                    if (order.Orderer.OrderedLaptops == null)
                    {
                        order.Orderer.OrderedLaptops = new List<Laptop>();
                    }

                    if (order.Laptop != null)
                    {
                        //add the ordered laptops to the orderer:
                        order.Orderer.OrderedLaptops.Add(order.Laptop);
                    }
                }

            }


            foreach (var laptop in mockLaptopData)
            {
                // set up the laptop's orders
                laptop.Orders = mockOrderData.Where(o => o.LaptopId == laptop.Id).ToList();
            }

            foreach (var orderer in mockOrdererData)
            {
                // set up the orderers orders
                orderer.Orders = mockOrderData.Where(o => o.OrdererId == orderer.Id).ToList();
            }



            mockOrderRepository = new Mock<IOrderRepository>();
            mockOrderRepository.Setup(t => t.ReadAll()).Returns(mockOrderData);

            orderLogic = new OrderLogic(mockOrderRepository.Object);

        }


        [Test]
        public void OrderCreateTest()
        {
            var sample =
                new Order() { LaptopId = 15, OrdererId = 8, Date = DateTime.Parse("2023.07.06") };

            orderLogic.Create(sample);

            mockOrderRepository.Verify(t => t.Create(sample), Times.Once);
        }

        [Test]
        public void OrderReadTestWithIncorrectId()
        {
            // there is no item with id: -1 !
            Assert.That(() => orderLogic.Read(-1), Throws.TypeOf<ArgumentException>());
        }


        [Test]
        public void MostPayingOrderersMethodTest()
        {
            var expected = new List<Orderer>()
            {
                new Orderer() {Id = 8 , Name = "Limbikani Cahaya", PhoneNumber = "06706927456" },
                new Orderer() {Id = 3 , Name = "Paschalis Nanda", PhoneNumber = "062001223112" },
                new Orderer() {Id = 4 , Name = "Linda Berenice", PhoneNumber = "06208293112" }
            };

            var result = orderLogic.MostPayingOrderers();

            
            Assert.AreEqual(result,expected);
        }

        [Test]
        public void MostPopularBrandsMethodTest()
        {
            var expected = new List<Brand>()
            {
                new Brand() {Id = 2 , Name = "Asus", YearOfAppearance = 1989  },
                new Brand() {Id = 3, Name = "Hp", YearOfAppearance =  1939},
                new Brand() {Id = 1, Name = "Apple", YearOfAppearance = 1976}
            };

            var result = orderLogic.MostPopularBrands();
            
            Assert.AreEqual (result,expected);
        }

        [Test]
        public void MostPopularLaptopModelsMethodTest()
        {
            var expected = new List<Laptop>()
            {
                new Laptop(){Id = 9, ModelName ="255 G9",Processor="AMD Ryzen 5", RAM = 16, RAM_Upgradeable = false, Storage = 512, BrandId = 3, Price = 250000 },
                new Laptop(){Id = 15, ModelName ="Blade 17",Processor="Intel Core i9", RAM = 32, RAM_Upgradeable = true, Storage = 1000, BrandId = 8 , Price = 1750000},
                new Laptop(){Id = 20, ModelName ="Inspiron 3520",Processor="Intel Core i5", RAM = 8, RAM_Upgradeable = true, Storage = 512, BrandId = 4 , Price = 185000}
            };

            var result = orderLogic.MostPopularLaptopModels();
            

            Assert.AreEqual(result,expected);
        }


        [Test]
        public void GetStatByYearMethodTest2023()
        {
            var expected = new List<YearInfo>()
            {
                new YearInfo() {Year = 2023 , Month = 2, IncomeByMonth = 500000 },
                new YearInfo() {Year = 2023 , Month = 6, IncomeByMonth = 898900 },
                new YearInfo() {Year = 2023 , Month = 9, IncomeByMonth = 4864900 },
                new YearInfo() {Year = 2023 , Month = 10, IncomeByMonth = 7000000 },
                new YearInfo() {Year = 2023 , Month = 11, IncomeByMonth = 756000 },
                new YearInfo() {Year = 2023 , Month = 12, IncomeByMonth = 1161000 },
                new YearInfo() {Year = 2023 , Month = 5, IncomeByMonth = 500000 },
                new YearInfo() {Year = 2023 , Month = 8, IncomeByMonth = 550000 },
                new YearInfo() {Year = 2023 , Month = 4, IncomeByMonth = 69900 } 
            };

            var result = orderLogic.GetStatByYear(2023);
            

            Assert.AreEqual(result, expected);
        }



    }
}
