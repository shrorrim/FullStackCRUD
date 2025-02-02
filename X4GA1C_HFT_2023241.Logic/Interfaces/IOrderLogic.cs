﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4GA1C_HFT_2023241.Models;

namespace X4GA1C_HFT_2023241.Logic
{
    public interface IOrderLogic
    {
        // crud methods:
        void Create(Order item);
        void Delete(int id);
        Order Read(int id);
        IEnumerable<Order> ReadAll();
        void Update(Order item);

        //non-crud methods:

        public IEnumerable<YearInfo> GetStatByYear(int year);

        public IEnumerable<Laptop> MostPopularLaptopModels();

        public IEnumerable<Brand> MostPopularBrands();

        public IEnumerable<Orderer> MostPayingOrderers();

    }
}
