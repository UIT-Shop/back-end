using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<OrderOverviewResponse>>>> GetOrders()
        {
            var result = await _orderService.GetOrders();
            return Ok(result);
        }

        [HttpGet("admin")]
        public async Task<ActionResult<ServiceResponse<OrdersAdminResponse>>> GetOrdersAdmin([Required] int page, [Required] Status status, [Required] DateTime monthYear)
        {
            var result = await _orderService.GetOrdersAdmin(page, status, monthYear);
            return Ok(result);
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<ServiceResponse<OrderDetailsResponse>>> GetOrdersDetails(int orderId)
        {
            var result = await _orderService.GetOrderDetails(orderId);
            return Ok(result);
        }

        [HttpPost()]
        public async Task<ActionResult<ServiceResponse<OrderDetailsResponse>>> PlaceOrder(OrderInput orderInput)
        {
            var address = new Address() { WardId = orderInput.WardId, Street = orderInput.street };
            var result = await _orderService.PlaceOrder(orderInput.Name, orderInput.Phone, address);
            return result.Success == false ? (ActionResult<ServiceResponse<OrderDetailsResponse>>)BadRequest(result) : (ActionResult<ServiceResponse<OrderDetailsResponse>>)Ok(result);
        }

        [HttpPut("{orderId}/{status}")]
        public async Task<ActionResult<ServiceResponse<OrderDetailsResponse>>> UpdateStatus(int orderId, Status status)
        {
            var result = await _orderService.UpdateStatus(orderId, status);
            return Ok(result);
        }
    }
}
