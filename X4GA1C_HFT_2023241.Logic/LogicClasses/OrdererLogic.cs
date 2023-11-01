using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4GA1C_HFT_2023241.Models;
using X4GA1C_HFT_2023241.Repository;

namespace X4GA1C_HFT_2023241.Logic
{
    public class OrdererLogic : IOrdererLogic
    {
        IOrdererRepository repository;

        public OrdererLogic(IOrdererRepository repo)
        {
            this.repository = repo;
        }
        public void Create(Orderer item)
        {
            this.repository.Create(item);
        }

        public void Delete(int id)
        {
            this.repository.Delete(id);
        }

        public Orderer Read(int id)
        {
            var temp = this.repository.Read(id);
            if (temp == null)
            {
                throw new ArgumentException("Orderer does not exist!");
            }

            return temp;
        }

        public IEnumerable<Orderer> ReadAll()
        {
            return (IEnumerable<Orderer>)this.repository.ReadAll();
        }

        public void Update(Orderer item)
        {
            this.repository.Update(item);
        }

        //non CRUD methods:


    }
}
