using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4GA1C_HFT_2023241.Models;

namespace X4GA1C_HFT_2023241.Repository.Repositories
{
    public class OrdererRepository : Repository<Orderer> , IOrdererRepository
    {

        public OrdererRepository(LaptopWebShopDbContext db ) : base(db)
        {
            
        }

        public override Orderer Read(int id)
        {
            return database.Orderers
                .FirstOrDefault(element => element.Id == id);
        }

        public override void Update(Orderer item)
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
