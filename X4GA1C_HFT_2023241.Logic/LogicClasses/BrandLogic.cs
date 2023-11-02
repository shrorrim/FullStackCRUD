using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using X4GA1C_HFT_2023241.Models;
using X4GA1C_HFT_2023241.Repository;

namespace X4GA1C_HFT_2023241.Logic
{
    public class BrandLogic : IBrandLogic
    {
        IBrandRepository repository;

        public BrandLogic(IBrandRepository repository)
        {
            this.repository = repository;
        }
        public void Create(Brand item)
        {
            if (item.Name.Length == 0)
            {
                throw new ArgumentException("Brand name incorrect...");
            }

            this.repository.Create(item);
        }

        public void Delete(int id)
        {
            this.repository.Delete(id);
        }

        public Brand Read(int id)
        {
            var temp = this.repository.Read(id);

            if (temp == null)
            {
                throw new ArgumentException("Brand does not exist!");
            }

            return temp;
        }

        public IEnumerable<Brand> ReadAll()
        {
            return (IEnumerable<Brand>)this.repository.ReadAll();
        }

        public void Update(Brand item)
        {
            this.repository.Update(item);
        }


        // non CRUD methods:


        public void Valami ()
        {
            var temp = this.repository.ReadAll().SelectMany(t => t.Laptops);
        }
    }
}
