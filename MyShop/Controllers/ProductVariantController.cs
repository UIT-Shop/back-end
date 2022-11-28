using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductVariantController : ControllerBase
    {
        private readonly IProductVariantService _productVariantService;

        public ProductVariantController(IProductVariantService productVariantService)
        {
            _productVariantService = productVariantService;
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<ServiceResponse<List<ProductVariant>>>> GetProductVariants(int productId)
        {
            var response = await _productVariantService.GetProductVariants(productId);
            return Ok(response);
        }

        [HttpGet("{productId}/{productSize}/{productColorId}")]
        public async Task<ActionResult<ServiceResponse<ProductVariant>>> GetProductVariant(int productId, string productSize, int productColorId)
        {
            var response = await _productVariantService.GetProductVariant(productId, productColorId, productSize);
            return Ok(response);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<ProductVariant>>>> AddProductVariant(ProductVariant productVariant)
        {
            var response = await _productVariantService.AddProductVariant(productVariant);
            return Ok(response);
        }

        [HttpPut, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<ProductVariant>>>> UpdateProductVariant(ProductVariant productVariant)
        {
            var response = await _productVariantService.UpdateProductVariant(productVariant);
            return Ok(response);
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteProductVariant(int id)
        {
            var result = await _productVariantService.DeleteProductVariant(id);
            return Ok(result);
        }
    }
}
