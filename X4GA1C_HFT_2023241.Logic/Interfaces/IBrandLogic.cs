using System.Collections.Generic;
using X4GA1C_HFT_2023241.Models;

namespace X4GA1C_HFT_2023241.Logic
{
    public interface IBrandLogic
    {
        void Create(Brand item);
        void Delete(int id);
        Brand Read(int id);
        IEnumerable<Brand> ReadAll();
        void Update(Brand item);
    }
}