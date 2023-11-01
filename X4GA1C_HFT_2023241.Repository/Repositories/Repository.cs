using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4GA1C_HFT_2023241.Models;

namespace X4GA1C_HFT_2023241.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {

        protected LaptopWebShopDbContext database;

        public Repository( LaptopWebShopDbContext database )
        {
              this.database = database ?? throw new ArgumentException( nameof( database ) );
        }

        public void Create(T item)
        {
            database.Set<T>().Add( item );
            database.SaveChanges();
        }

        public void Delete(int id)
        {
            database.Set<T>().Remove(Read(id));
            database.SaveChanges();
        }

        public T Read(int id)
        {
            return  database.Set<T>().FirstOrDefault(element => element.Id == id);
        }

        public IQueryable<T> ReadAll()
        {
            return database.Set<T>();
        }

        public virtual void Update(T item)
        {
            var old = Read(item.Id);

            foreach (var prop in old.GetType().GetProperties() )
            {
                prop.SetValue(old, prop.GetValue(item));
            }

            database.SaveChanges();
        }
    }
}
