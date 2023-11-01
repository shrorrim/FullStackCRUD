using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4GA1C_HFT_2023241.Models;

namespace X4GA1C_HFT_2023241.Repository.Repositories
{
    public class OrderRepository : Repository<Order> , IOrderRepository
    {

        public OrderRepository(LaptopWebShopDbContext db) : base(db)
        {
                
        }

        public override Order Read(int id)
        {
            return database.Orders
                .FirstOrDefault(order => order.Id == id);
        }

        public override void Update(Order item)
        {
            var old = Read(item.Id);

            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }

            database.SaveChanges();
        }
    }
}
