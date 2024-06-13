using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using X4GA1C_HFT_2023241.Endpoint.Services;
using X4GA1C_HFT_2023241.Logic;
using X4GA1C_HFT_2023241.Models;


namespace X4GA1C_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderLogic logic;
        IHubContext<SignalRHub> hub;

        public OrderController(IOrderLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }


        [HttpGet]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IEnumerable<Order> ReadAll()
        {
            return this.logic.ReadAll();
        }


        [HttpGet("{id}")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public Order Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Order value)
        {
            this.logic.Create(value);
            this.hub.Clients
                .All.SendAsync("OrderCreated", value);
        }


        [HttpPut]
        public void Update([FromBody] Order value)
        {
            this.logic.Update(value);
            this.hub.Clients
                .All.SendAsync("OrderUpdated", value);
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var temp = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients
                .All.SendAsync("OrderDeleted", temp);
        }
    }
}
