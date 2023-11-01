using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4GA1C_HFT_2023241.Models;

namespace X4GA1C_HFT_2023241.Logic
{
    public interface ILaptopLogic
    {
        void Create(Laptop item);
        void Delete(int id);
        Laptop Read(int id);
        IEnumerable<Laptop> ReadAll();
        void Update(Laptop item);
    }
}
