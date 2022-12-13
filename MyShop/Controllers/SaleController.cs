using Microsoft.AspNetCore.Mvc;

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet("predict")]
        public ActionResult<ServiceResponse<PredictSale>> GetPredict()
        {
            var result = _saleService.GetPredict();
            return Ok(result);
        }

        [HttpGet("sale")]
        public async Task<ActionResult<ServiceResponse<List<Sale>>>> GetSales()
        {
            var result = await _saleService.GetSales();
            return Ok(result);
        }
    }
}
