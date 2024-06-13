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
    public class OrdererController : ControllerBase
    {
        IOrdererLogic logic;
        IHubContext<SignalRHub> hub;

        public OrdererController(IOrdererLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IEnumerable<Orderer> ReadAll()
        {
            return this.logic.ReadAll(); 
        }


        [HttpGet("{id}")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public Orderer Read(int id)
        {
            return this.logic.Read(id);
        }


        [HttpPost]
        public void Create([FromBody] Orderer value)
        {
            this.logic.Create(value);
            this.hub.Clients
                .All.SendAsync("OrdererCreated", value);
        }


        [HttpPut]
        public void Update([FromBody] Orderer value)
        {
            this.logic.Update(value);
            this.hub.Clients
                .All.SendAsync("OrdererUpdated", value);
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var temp = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients
                .All.SendAsync("OrdererDeleted", temp);
        }
    }
}
