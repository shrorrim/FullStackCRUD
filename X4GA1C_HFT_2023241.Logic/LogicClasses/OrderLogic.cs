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

        //public YearInfo GetOrdersByYearByMonth(int year)
        //{
        //    var temp =  from x in this.repository.ReadAll()
        //                where x.Date.Year == year
        //                let l = x.Laptop
        //                group x by x.Date.Month into g
        //                select new
        //                {
        //                      Year = year,
        //                      NumberOrders = g.Count(),
        //                      PriceByMonth = g.Sum(t =>t.Laptop.Price)
        //                };


        //    YearInfo info = new YearInfo();

        //    info.Year = year;
        //    info.TotalOrders = temp.Sum(t => t.NumberOrders);
        //    info.AvgOrdersByMonth = info.TotalOrders / 12;
        //    info.TotalIncome = temp.Sum(t =>t.PriceByMonth);
        //    info.AvgIncomeByMonth = info.TotalIncome / 12;

        //    return info;
        //}

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
            var max = this.repository.ReadAll().Select(t => t.Orderer).Select(t => t.OrderedLaptops.Sum(t => t.Price)).Distinct().ToList();

            max.Sort();
            max.Reverse();
            max.ToArray();
            
           
            //kelelne a 3 legtöbbet költött megrendelő
            
            var orderer = this.repository.ReadAll().Select(t => t.Orderer).Where(t =>t.OrderedLaptops.Sum(t =>t.Price) >= max[2] ).Distinct();

            
            return orderer.Take(3);
        }

    }

    public class YearInfo
    {

        public int Year { get; set; }
        public int TotalOrders { get; set; }
        public double AvgOrdersByMonth { get; set; }
        public int TotalIncome { get; set; }
        public double AvgIncomeByMonth { get; set; }
    }
}
