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
    }
}
