using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Brand>>>> GetBrands()
        {
            var result = await _brandService.GetBrands();
            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<ActionResult<ServiceResponse<Brand>>> GetBrand(int id)
        {
            var response = await _brandService.GetBrand(id);
            return !response.Success ? (ActionResult<ServiceResponse<Brand>>)BadRequest(response) : (ActionResult<ServiceResponse<Brand>>)Ok(response);

        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<Product>>> CreateBrand(Brand brand)
        {
            var result = await _brandService.CreateBrand(brand);
            return Ok(result);
        }

        [HttpPut, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<Product>>> UpdateBrand(Brand brand)
        {
            var result = await _brandService.UpdateBrand(brand);
            return Ok(result);
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteBrand(int id)
        {
            var result = await _brandService.DeleteBrand(id);
            return Ok(result);
        }
    }
}
