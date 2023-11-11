using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using X4GA1C_HFT_2023241.Logic;
using X4GA1C_HFT_2023241.Models;

namespace X4GA1C_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {

        // order/laptop logic needed:

        ILaptopLogic laptopLogic;

        IOrderLogic orderLogic;

        public StatController(ILaptopLogic laptopLogic, IOrderLogic orderLogic)
        {
            this.laptopLogic = laptopLogic;
            this.orderLogic = orderLogic;
        }


        [HttpGet]
        public IEnumerable<KeyValuePair<string, double>> AvgPriceByBrands()
        {
            return this.laptopLogic.AvgPriceByBrands();
        }


        [HttpGet]
        public IEnumerable<Orderer> MostPayingOrderers()
        {
            return this.orderLogic.MostPayingOrderers();
        }

        [HttpGet]
        public IEnumerable<Brand> MostPopularBrands()
        {
            return this.orderLogic.MostPopularBrands();
        }

        [HttpGet]
        public IEnumerable<Laptop> MostPopularLaptopModels()
        {
            return this.orderLogic.MostPopularLaptopModels();
        }

        [HttpGet("{year}")]
        public IEnumerable<YearInfo> GetStatByYear(int year)
        {
            return this.orderLogic.GetStatByYear(year);
        }

    }
}
