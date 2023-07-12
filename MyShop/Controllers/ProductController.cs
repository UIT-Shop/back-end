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
        private readonly IRatingService _commentService;
        private readonly IRatingService _ratingService;

        public ProductController(IProductService productService, IRatingService commentService, IRatingService ratingService)
        {
            _productService = productService;
            _commentService = commentService;
            _ratingService = ratingService;
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

        [HttpGet("training")]
        public ActionResult<ServiceResponse<bool>> Traning()
        {
            _ratingService.ReTrainData();
            return new ServiceResponse<bool> { Data = true };
        }

        [HttpGet("recommend"), Authorize]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> Recommend()
        {
            var productIds = await _productService.GetListProductIds();
            var recommendOutputs = _ratingService.GetRecommend(productIds.Data);
            List<int> recommendProductIds = new List<int>();
            Console.WriteLine("=============== Top recommend ===============");
            Console.WriteLine("ProductId\t\t\tPredictScore");
            foreach (var recommendOutput in recommendOutputs)
            {
                recommendProductIds.Add(recommendOutput.ProductId);
                Console.WriteLine($"{recommendOutput.ProductId}\t\t\t{recommendOutput.Score}");
            }
            var result = await _productService.GetProducts(recommendProductIds);
            return (ActionResult<ServiceResponse<List<Product>>>)Ok(result);
        }

        [HttpGet("{productId:int}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetProduct(int productId)
        {
            var result = await _productService.GetProductById(productId);
            return !result.Success ? (ActionResult<ServiceResponse<Product>>)BadRequest(result) : (ActionResult<ServiceResponse<Product>>)Ok(result);
        }

        [HttpGet("ratingSummary/{productId:int}")]
        public async Task<ActionResult<ServiceResponse<List<RatingCount>>>> GetSummaryRating(int productId)
        {
            var result = await _ratingService.GetSummaryRating(productId);
            return !result.Success ? (ActionResult<ServiceResponse<List<RatingCount>>>)BadRequest(result) : (ActionResult<ServiceResponse<List<RatingCount>>>)Ok(result);
        }

        [HttpGet("topSale")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetTopSaleProducts()
        {
            var result = await _productService.GetTopSaleProducts();
            return !result.Success ? (ActionResult<ServiceResponse<List<Product>>>)BadRequest(result) : (ActionResult<ServiceResponse<List<Product>>>)Ok(result);
        }

        [HttpGet("Category/{categoryId}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetProductsByCategory(int categoryId, [Required] int page)
        {
            var result = await _productService.GetProductsByCategory(categoryId, page);
            return !result.Success ? (ActionResult<ServiceResponse<Product>>)BadRequest(result) : (ActionResult<ServiceResponse<Product>>)Ok(result);
        }

        [HttpGet("search/{searchText}/{page}/{orderPrice}")]
        public async Task<ActionResult<ServiceResponse<ProductSearchResult>>> SearchProducts(string searchText, int page, int orderPrice)
        {
            var result = await _productService.SearchProducts(searchText, page, orderPrice);

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
