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
    public class BrandController : ControllerBase
    {
        IBrandLogic logic;
        IHubContext<SignalRHub> hub;

        public BrandController(IBrandLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IEnumerable<Brand> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public Brand Read(int id)
        {
            return this.logic.Read(id);
        }


        [HttpPost]
        public void Create([FromBody] Brand value)
        {
            this.logic.Create(value);
            this.hub.Clients
                .All.SendAsync("BrandCreated", value);
        }


        [HttpPut]
        public void Update([FromBody] Brand value)
        {
            this.logic.Update(value);
            this.hub.Clients
                .All.SendAsync("BrandUpdated", value);
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var temp = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients
                .All.SendAsync("BrandDeleted", temp);
        }
    }
}
