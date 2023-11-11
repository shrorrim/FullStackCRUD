using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using X4GA1C_HFT_2023241.Logic;
using X4GA1C_HFT_2023241.Models;

namespace X4GA1C_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrdererController : ControllerBase
    {
        IOrdererLogic logic;

        public OrdererController(IOrdererLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<Orderer> ReadAll()
        {
            return this.logic.ReadAll(); 
        }


        [HttpGet("{id}")]
        public Orderer Read(int id)
        {
            return this.logic.Read(id);
        }


        [HttpPost]
        public void Create([FromBody] Orderer value)
        {
            this.logic.Create(value);
        }


        [HttpPut]
        public void Update([FromBody] Orderer value)
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
