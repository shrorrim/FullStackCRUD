using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4GA1C_HFT_2023241.Models;
using X4GA1C_HFT_2023241.Repository;

namespace X4GA1C_HFT_2023241.Logic
{
    public class LaptopLogic : ILaptopLogic
    {
        ILaptopRepository repository;

        public LaptopLogic(ILaptopRepository repository)
        {
            this.repository = repository;
        }


        public void Create(Laptop item)
        {
            if (item.Price < 0 )
            {
                throw new ArgumentException("Price can not be negative!");
            }

            this.repository.Create(item);
        }

        public void Delete(int id)
        {
            this.repository.Delete(id);
        }

        public Laptop Read(int id)
        {
            var temp = this.repository.Read(id);
            if (temp == null)
            {
                throw new ArgumentException("Laptop does not exist!");
            }
            
            return temp;
        }

        public IEnumerable<Laptop> ReadAll()
        {
            return (IEnumerable<Laptop>)this.repository.ReadAll();
        }

        public void Update(Laptop item)
        {
            this.repository.Update(item);
        }


        //non CRUD methods:

        public IEnumerable<KeyValuePair<string, double>> AvgPriceByBrands()
        {
            return from x in this.repository.ReadAll()
                       group x by x.Brand.Name into g
                       select new KeyValuePair<string, double>(g.Key, g.Average(z => z.Price));

        }
    }
}
