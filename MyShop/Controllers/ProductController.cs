using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("admin"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<ProductSearchResult>>> GetAdminProducts([Required] int page)
        {
            var result = await _productService.GetAdminProducts(page);
            return !result.Success ? (ActionResult<ServiceResponse<ProductSearchResult>>)BadRequest(result) : (ActionResult<ServiceResponse<ProductSearchResult>>)Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<ProductSearchResult>>> GetProducts([Required] int page)
        {
            var result = await _productService.GetProductsAsync(page);
            return !result.Success ? (ActionResult<ServiceResponse<ProductSearchResult>>)BadRequest(result) : (ActionResult<ServiceResponse<ProductSearchResult>>)Ok(result);
        }

        [HttpPost("test")]
        public async Task<ActionResult<ServiceResponse<string>>> Test()
        {
            var result = new ServiceResponse<string>() { Message = "Test", Data = null };
            return !result.Success ? (ActionResult<ServiceResponse<string>>)BadRequest(result) : (ActionResult<ServiceResponse<string>>)Ok(result);
        }

        [HttpGet("{productId:int}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetProduct(int productId)
        {
            var result = await _productService.GetProductById(productId);
            return !result.Success ? (ActionResult<ServiceResponse<Product>>)BadRequest(result) : (ActionResult<ServiceResponse<Product>>)Ok(result);
        }

        [HttpGet("Category/{categoryUrl}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetProductsByCategory(string categoryUrl)
        {
            var result = await _productService.GetProductsByCategory(categoryUrl);
            return !result.Success ? (ActionResult<ServiceResponse<Product>>)BadRequest(result) : (ActionResult<ServiceResponse<Product>>)Ok(result);
        }

        [HttpGet("search/{searchText}/{page}")]
        public async Task<ActionResult<ServiceResponse<ProductSearchResult>>> SearchProducts(string searchText, int page)
        {
            var result = await _productService.SearchProducts(searchText, page);

            return Ok(result);
        }

        [HttpGet("searchSuggestion/{searchText}")]
        public async Task<ActionResult<ServiceResponse<List<string>>>> SearchSuggestionProducts(string searchText)
        {
            var result = await _productService.SearchSuggestionProducts(searchText);
            return !result.Success ? (ActionResult<ServiceResponse<List<string>>>)BadRequest(result) : (ActionResult<ServiceResponse<List<string>>>)Ok(result);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<Product>>> CreateProduct(Product product)
        {
            var result = await _productService.CreateProduct(product);
            return !result.Success ? (ActionResult<ServiceResponse<Product>>)BadRequest(result) : (ActionResult<ServiceResponse<Product>>)Ok(result);
        }

        [HttpPut, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<Product>>> UpdateProduct(Product product)
        {
            var result = await _productService.UpdateProduct(product);
            return !result.Success ? (ActionResult<ServiceResponse<Product>>)BadRequest(result) : (ActionResult<ServiceResponse<Product>>)Ok(result);
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProduct(id);
            return !result.Success ? (ActionResult<ServiceResponse<bool>>)BadRequest(result) : (ActionResult<ServiceResponse<bool>>)Ok(result);
        }
    }
}
