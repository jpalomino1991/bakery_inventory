using Bakery.Inventory.DomainApi.Model;
using Bakery.Inventory.DomainApi.Port;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Bakery.Inventory.RestAdapter.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DealController : ControllerBase
    {
        private readonly IRequestDeal<Bakery.Inventory.DomainApi.Model.Inventory> _requestDeal;

        public DealController(IRequestDeal<Bakery.Inventory.DomainApi.Model.Inventory> requestDeal)
        {
            _requestDeal = requestDeal;
        }

        // GET: api/deal
        [HttpGet]
        public IActionResult Get()
        {
            var result = _requestDeal.GetDeals();
            return Ok(result);
        }

        // GET: api/deal/1
        [HttpGet]
        [Route("{id}", Name = "GetDeal")]
        public IActionResult Get(int id)
        {
            var result = _requestDeal.GetDeal(id);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddInventory([FromBody] Bakery.Inventory.DomainApi.Model.Inventory inventory)
        {
            var result = _requestDeal.AddValue(inventory);
            if (result == null)
                return BadRequest("Inventory already exists");
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult DeleteInventory([FromBody] Bakery.Inventory.DomainApi.Model.Inventory inventory)
        {
            try
            {
                var result = _requestDeal.DeleteValue(inventory);
                if (result == null)
                    return BadRequest("Inventory doesn't exists");
                return Ok(result);
            }
            catch(InvalidOperationException e)
            {
                return BadRequest("Stock is less than quantity");
            }
        }

        [HttpPut]
        public IActionResult UpdateInventory([FromBody] Bakery.Inventory.DomainApi.Model.Inventory inventory)
        {
            var result = _requestDeal.EditValue(inventory);
            if (result == null)
                return BadRequest("Inventory doesn't exists");
            return Ok(result);
        }
    }
}
