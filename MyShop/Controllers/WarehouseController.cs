using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Warehouse>>>> GetWarehouses()
        {
            var result = await _warehouseService.GetWarehouses();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Warehouse>>> GetWarehouse(int id)
        {
            var response = await _warehouseService.GetWarehouse(id);
            return !response.Success ? (ActionResult<ServiceResponse<Warehouse>>)BadRequest(response) : (ActionResult<ServiceResponse<Warehouse>>)Ok(response);

        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<Product>>> CreateWarehouse(Warehouse warehouse)
        {
            var result = await _warehouseService.CreateWarehouse(warehouse);
            return Ok(result);
        }

        [HttpPut, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<Product>>> UpdateWarehouse(Warehouse warehouse)
        {
            var result = await _warehouseService.UpdateWarehouse(warehouse);
            return Ok(result);
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteWarehouse(int id)
        {
            var result = await _warehouseService.DeleteWarehouse(id);
            return Ok(result);
        }
    }
}
