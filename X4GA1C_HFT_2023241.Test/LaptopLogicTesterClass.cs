using Castle.Components.DictionaryAdapter.Xml;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using X4GA1C_HFT_2023241.Logic;
using X4GA1C_HFT_2023241.Models;
using X4GA1C_HFT_2023241.Repository;

namespace X4GA1C_HFT_2023241.Test
{
    [TestFixture]
    public class LaptopLogicTesterClass
    {
        LaptopLogic laptopLogic;

        Mock<ILaptopRepository> mockLaptopRepository;


        [SetUp]
        public void Init()
        {
             var mockData = new List<Laptop>()
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


            // Create relationships between laptops and brands:
            foreach (var laptop in mockData)
            {
                laptop.Brand = mockBrandData.FirstOrDefault(b => b.Id == laptop.BrandId);
            }


            mockLaptopRepository = new Mock<ILaptopRepository>();
            mockLaptopRepository.Setup(t => t.ReadAll()).Returns(mockData);

            laptopLogic = new LaptopLogic(mockLaptopRepository.Object);
        }


        [Test]
        public void LaptopReadTestWithIncorrectId()
        {
            // there is no item with id: 800 !
            Assert.That(() => laptopLogic.Read(800), Throws.TypeOf<ArgumentException>());
        }


        [Test]
        public void LaptopCreateTestWithNegativePrice()
        {
            var sample = new Laptop() {ModelName = "Macbook Air 15", Price = -2000000 };

            //Assert:

            Assert.That(() => laptopLogic.Create(sample), Throws.TypeOf<ArgumentException>());

        }

        [Test]
        public void AvgPriceByBrandsTest() // non crud test
        {
            var expected = new List<KeyValuePair<string, double>>()
            {
                new KeyValuePair<string, double>("Asus",710699.857),
                new KeyValuePair<string, double>("Apple",1009000),
                new KeyValuePair<string, double>("Hp",399633.333),
                new KeyValuePair<string, double>("Dell",449999.667),
                new KeyValuePair<string, double>("Acer",249633.333),
                new KeyValuePair<string, double>("Lenovo",442225),
                new KeyValuePair<string, double>("Msi",600000),
                new KeyValuePair<string, double>("Razer",1750000)
            };


            //Act:
            var result = laptopLogic.AvgPriceByBrands();


            //Assert:

            Assert.AreEqual(expected, result);
        }

    }
}
