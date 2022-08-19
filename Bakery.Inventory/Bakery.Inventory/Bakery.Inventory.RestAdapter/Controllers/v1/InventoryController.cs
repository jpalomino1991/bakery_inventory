using Bakery.Inventory.DomainApi.Port;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bakery.Inventory.RestAdapter.Controllers.v1
{
    [Authorize]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IRequestInventory<Bakery.Inventory.DomainApi.Model.Inventory> _requestInventory;

        public InventoryController(IRequestInventory<Bakery.Inventory.DomainApi.Model.Inventory> requestInventory)
        {
            _requestInventory = requestInventory;
        }

        [HttpGet]
        public IActionResult GetInventories()
        {
            var inventories = _requestInventory.GetValues();
            return Ok(inventories);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetInventory(int id)
        {
            var inventories = _requestInventory.GetValue(id);
            return Ok(inventories);
        }

        [HttpPost]
        public IActionResult AddInventory([FromBody] Bakery.Inventory.DomainApi.Model.Inventory inventory)
        {
            if (User != null)
                inventory.User = User.Identity.Name;
            var result = _requestInventory.AddValue(inventory);
            if (result == null)
                return BadRequest("Inventory already exists");
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult DeleteInventory([FromBody] Bakery.Inventory.DomainApi.Model.Inventory inventory)
        {
            try
            {
                var result = _requestInventory.DeleteValue(inventory);
                if (result == null)
                    return BadRequest("Inventory doesn't exists");
                return Ok(result);
            }
            catch
            {
                return BadRequest("Stock is less than quantity");
            }
        }

        [HttpPut]
        public IActionResult UpdateInventory([FromBody] Bakery.Inventory.DomainApi.Model.Inventory inventory)
        {
            if (User != null)
                inventory.User = User.Identity.Name;
            var result = _requestInventory.EditValue(inventory);
            if (result == null)
                return BadRequest("Inventory doesn't exists");
            return Ok(result);
        }
    }
}
