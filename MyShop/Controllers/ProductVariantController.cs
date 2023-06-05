using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductVariantController : ControllerBase
    {
        private readonly IProductVariantService _productVariantService;
        private readonly IProductVariantStoreService _productVariantStoreService;

        public ProductVariantController(IProductVariantService productVariantService, IProductVariantStoreService productVariantStoreService)
        {
            _productVariantService = productVariantService;
            _productVariantStoreService = productVariantStoreService;
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

        [HttpPost("Store"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> AddProductVariantToWarehouse(ProductVariantStoreInput productVariantStoreInput)
        {
            var result = await _productVariantStoreService.AddProductVariantStore(productVariantStoreInput);
            return result.Success == false ? (ActionResult<ServiceResponse<bool>>)BadRequest(result) : (ActionResult<ServiceResponse<bool>>)Ok(result);
        }

        [HttpPut("Store"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> MoveProductVariantToWarehouse(ProductVariantStoreInput productVariantStoreInput)
        {
            var result = await _productVariantStoreService.MoveProductVariantStore(productVariantStoreInput);
            return result.Success == false ? (ActionResult<ServiceResponse<bool>>)BadRequest(result) : (ActionResult<ServiceResponse<bool>>)Ok(result);
        }

        [HttpGet("StoreByProduct/{productId}/{monthYear}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<ProductVariantStoreOutput>>>> GetProductVariantStoresByProduct(int productId, DateTime monthYear)
        {
            var result = await _productVariantStoreService.GetProductVariantStoresByProduct(productId, monthYear);
            return result.Success == false ? (ActionResult<ServiceResponse<List<ProductVariantStoreOutput>>>)BadRequest(result) : (ActionResult<ServiceResponse<List<ProductVariantStoreOutput>>>)Ok(result);
        }

        [HttpGet("StoreByWarehouse/{warehouseId}/{monthYear}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<ProductVariantStoreOutput>>>> GetProductVariantStoresByWarehouse(int warehouseId, DateTime monthYear)
        {
            var result = await _productVariantStoreService.GetProductVariantStoresByWarehouse(warehouseId, monthYear);
            return result.Success == false ? (ActionResult<ServiceResponse<List<ProductVariantStoreOutput>>>)BadRequest(result) : (ActionResult<ServiceResponse<List<ProductVariantStoreOutput>>>)Ok(result);
        }
    }
}
