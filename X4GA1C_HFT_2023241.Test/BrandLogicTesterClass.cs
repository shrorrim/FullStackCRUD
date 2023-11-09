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

namespace X4GA1C_HFT_2023241.Test
{
    [TestFixture]
    public class BrandLogicTesterClass
    {
        BrandLogic brandLogic;
        Mock<IBrandRepository> mockBrandRepository;

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

            mockBrandRepository = new Mock<IBrandRepository>();
            mockBrandRepository.Setup(t=>t.ReadAll()).Returns(mockBrandData);

            brandLogic = new BrandLogic(mockBrandRepository.Object);
        }


        [Test]
        public void BrandCreateTestWithIncorrectName()
        {
            var sample = new Brand() {Name = "" };

            Assert.That(() => brandLogic.Create(sample), Throws.TypeOf<ArgumentException>());
        }

    }
}
