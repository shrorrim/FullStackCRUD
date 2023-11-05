using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            return (from x in this.repository.ReadAll()
                        where x.Date.Year == year
                        select new YearInfo()
                        {
                            Year = year,
                            Month = x.Date.Month,
                            IncomeByMonth = x.Orderer.OrderedLaptops.Sum(t=>t.Price)
                        }).Distinct().OrderBy(t=>t.Month);
        }

        // popular brand:

        public IEnumerable<Brand> MostPopularBrands()
        {
            var temp =  (from x in this.repository.ReadAll()
                        group x by x.Laptop.Brand.Name into g
                        orderby g.Count() descending
                        select new
                        {
                            Brand = g.Key
                        }).Select(t => t.Brand).Distinct().Take(3).ToList();

            return (IEnumerable<Brand>)this.repository.ReadAll().Select(t => t.Laptop.Brand).Where(t => temp.Contains(t.Name)).Distinct();
        }

        //orderer who spent the most:

        public IEnumerable<Orderer> MostPayingOrderers()
        {
            var maxSpending = (
            from x in this.repository.ReadAll()
            select new
            {
                Sum = x.Orderer.OrderedLaptops.Sum(t => t.Price),
                Orderer = x.Orderer
            }).OrderByDescending(t=>t.Sum).Distinct().Take(3).Select(t=>t.Orderer);
            

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
    }
}
