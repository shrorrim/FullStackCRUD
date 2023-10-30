using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4GA1C_HFT_2023241.Models;

namespace X4GA1C_HFT_2023241.Repository
{
    public interface IRepository<T> where T : Entity
    {
        void Create(T item);

        void Update(T item);

        void Delete(int id);


        T Read(int id);

        IQueryable<T> ReadAll();

    }
}
