using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using X4GA1C_HFT_2023241.Logic;
using X4GA1C_HFT_2023241.Models;


namespace X4GA1C_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderLogic logic;

        public OrderController(IOrderLogic logic)
        {
            this.logic = logic;
        }


        [HttpGet]
        public IEnumerable<Order> ReadAll()
        {
            return this.logic.ReadAll();
        }


        [HttpGet("{id}")]
        public Order Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Order value)
        {
            this.logic.Create(value);
        }


        [HttpPut]
        public void Update([FromBody] Order value)
        {
            this.logic.Update(value);
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
