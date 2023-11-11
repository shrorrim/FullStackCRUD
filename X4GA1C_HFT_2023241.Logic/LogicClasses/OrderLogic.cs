using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using X4GA1C_HFT_2023241.Models;
using X4GA1C_HFT_2023241.Repository;

namespace X4GA1C_HFT_2023241.Logic
{
    public class OrderLogic : IOrderLogic
    {
        IOrderRepository repository;

        public OrderLogic( IOrderRepository repo )
        {
            this.repository = repo;
        }

        public void Create(Order item)
        {
            this.repository.Create( item );
        }

        public void Delete(int id)
        {
            this.repository.Delete(id);
        }

        public Order Read(int id)
        {
            var temp = this.repository.Read(id);
            if (temp == null)
            {
                throw new ArgumentException("Order does not exist!");
            }

            return temp;
        }

        public IEnumerable<Order> ReadAll()
        {
           return (IEnumerable<Order>)this.repository.ReadAll();
        }

        public void Update(Order item)
        {
            this.repository.Update(item);
        }



        //non CRUD methods:

        public IEnumerable<YearInfo> GetStatByYear(int year)
        {
            var result = (from x in this.repository.ReadAll()
                          join laptop in this.repository.ReadAll().Select(t => t.Laptop) on x.LaptopId equals laptop.Id
                          where x.Date.Year == year
                          group new { x, laptop } by x.Date.Month into g
                          select new YearInfo()
                          {
                              Year = year,
                              Month = g.Key,
                              IncomeByMonth = g.Sum(item => item.laptop.Price)
                          });

            
            return (IEnumerable<YearInfo>)result;
        }

        //top 3 model:

        public IEnumerable<Laptop> MostPopularLaptopModels()
        {
            var q = (from x in this.repository.ReadAll()
                    join laptop in this.repository.ReadAll().Select(t => t.Laptop) on
                    x.Laptop.Id equals laptop.Id
                    group x by new { laptop.Id, laptop.ModelName, laptop.Processor, laptop.RAM, laptop.Storage, laptop.RAM_Upgradeable, laptop.Price , laptop.BrandId} into g
                    orderby g.Count() descending
                    select new
                    {
                        Id = g.Key.Id,
                        ModelName = g.Key.ModelName,
                        Processor = g.Key.Processor,
                        RAM = g.Key.RAM,
                        Storage = g.Key.Storage,
                        RAM_Upgradeable = g.Key.RAM_Upgradeable,
                        Price = g.Key.Price,
                        BrandId = g.Key.BrandId
                    }).Take(3).Select(t=> new Laptop() { Id = t.Id, ModelName = t.ModelName,
                        Processor = t.Processor, RAM = t.RAM, Storage= t.Storage, RAM_Upgradeable = t.RAM_Upgradeable,
                        Price = t.Price, BrandId = t.BrandId } );
            
            
            return (IEnumerable<Laptop>)q;
        }

        // top 3 brand:

        public IEnumerable<Brand> MostPopularBrands()
        {

            var q = (from x in this.repository.ReadAll()
                        join brand in this.repository.ReadAll().Select(t => t.Laptop.Brand) on
                        x.Laptop.Brand.Id equals brand.Id
                        group x by new { brand.Id, brand.Name, brand.YearOfAppearance } into g
                        orderby g.Count() descending
                        select new Brand()
                        {
                            Id = g.Key.Id,
                            Name = g.Key.Name,
                            YearOfAppearance = g.Key.YearOfAppearance
                        }).Take(3);


            

            return (IEnumerable<Brand>)q;
        }

        //top 3 orderer who spent the most:

        public IEnumerable<Orderer> MostPayingOrderers()
        {
            var maxSpending = (
            from x in this.repository.ReadAll()
            select new
            {
                Sum = x.Orderer.OrderedLaptops.Sum(t => t.Price),
                Orderer = x.Orderer
            }).Distinct().OrderByDescending(t=>t.Sum).Take(3).Select(t=>t.Orderer);

            
            return (IEnumerable<Orderer>)maxSpending;
        }

    }

    public class YearInfo
    {

        public int Year { get; set; }
        public int Month { get; set; }
        public int IncomeByMonth { get; set; }

        public override string ToString()
        {
            return $"Year: {Year}, Month: {Month}, Income: {IncomeByMonth}";
        }

        public override bool Equals(object obj)
        {
            return this.Year == (obj as YearInfo).Year 
                   && this.Month == (obj as YearInfo).Month
                   && this.IncomeByMonth == (obj as YearInfo).IncomeByMonth;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Year, this.Month, this.IncomeByMonth );
        }

    }
}
