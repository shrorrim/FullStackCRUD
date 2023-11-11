using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4GA1C_HFT_2023241.Models;

namespace X4GA1C_HFT_2023241.Repository.Repositories
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        public BrandRepository(LaptopWebShopDbContext db )
            :base(db)
        {
                
        }

        public override Brand Read(int id)
        {
            return database.Brands
                .FirstOrDefault(element => element.Id == id);
        }

        public override void Update(Brand item)
        {
            var old = Read(item.Id);

            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t=>t.IsVirtual) == null)
                {
                    if (prop.GetValue(item) != null)
                    {
                        prop.SetValue(old, prop.GetValue(item));
                    }
                   
                }
                
            }

            database.SaveChanges();
        }
    }
}
