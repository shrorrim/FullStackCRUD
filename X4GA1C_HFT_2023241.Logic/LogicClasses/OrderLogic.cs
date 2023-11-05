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
             return this.repository.ReadAll()
            .Where(x => x.Date.Year == year)
            .ToList()
            .GroupBy(x => new { x.Date.Year, x.Date.Month })
            .Select(g => new YearInfo()
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                OrdersByMonth = g.Count(),
                IncomeByMonth = g.Sum(x => x.Laptop.Price),
            });

        }

        // popular brand:

        public Brand MostPopularBrand()
        {
            var temp = from x in this.repository.ReadAll()
                       group x by x.Laptop.Brand.Name into g
                       orderby g.Count() descending
                       select new Brand()
                       {
                           Name = g.Key
                       };

            return (Brand)temp.First();
        }

        //orderer who spent the most:

        public IEnumerable<Orderer> MostPayingOrderers()
        {
            return this.repository.ReadAll()
                       .Include(order => order.Orderer)
                       .Include(order => order.Laptop)
                       .ToList()
                       .GroupBy(order => order.Orderer)
                       .Select(orderGroup => new
                       {
                             Orderer = orderGroup.Key,
                             TotalSpending = orderGroup.Sum(order => order.Laptop.Price)
                       })
                       .OrderByDescending(item => item.TotalSpending)
                       .Take(3)
                       .Select(item => item.Orderer);
        }

    }

    public class YearInfo
    {

        public int Year { get; set; }

        public int Month { get; set; }
        public int OrdersByMonth { get; set; }
        public int IncomeByMonth { get; set; }

        public override string ToString()
        {
            return $"Year: {Year}, Month: {Month}, NumberOfOrders: {OrdersByMonth}, Income: {IncomeByMonth}";
        }
    }
}
