using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4GA1C_HFT_2023241.Models;

namespace X4GA1C_HFT_2023241.Repository.Repositories
{
    public class LaptopRepository : Repository<Laptop> , ILaptopRepository
    {
        public LaptopRepository(LaptopWebShopDbContext db) : base(db)
        {
                
        }

        public override Laptop Read(int id)
        {
            return database.Laptops
                .FirstOrDefault(element => element.Id == id);
        }

        public override void Update(Laptop item)
        {
            var old = Read(item.Id);

            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                
                        prop.SetValue(old, prop.GetValue(item));
                    
                    
                }
            }

            database.SaveChanges();
        }
    }
}
