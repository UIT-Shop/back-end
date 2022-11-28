using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Category>>>> GetCategories()
        {
            var result = await _categoryService.GetCategories();
            return Ok(result);
        }

        [HttpGet("gender/{gender}")]
        public async Task<ActionResult<ServiceResponse<List<Category>>>> GetCategory(string gender)
        {
            var response = await _categoryService.GetCategories(gender);
            return !response.Success ? (ActionResult<ServiceResponse<List<Category>>>)BadRequest(response) : (ActionResult<ServiceResponse<List<Category>>>)Ok(response);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Category>>> GetCategory(int id)
        {
            var response = await _categoryService.GetCategory(id);
            return !response.Success ? (ActionResult<ServiceResponse<Category>>)BadRequest(response) : (ActionResult<ServiceResponse<Category>>)Ok(response);

        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<Category>>> CreateCategory(Category category)
        {
            if (category == null || category.Name.Length == 0 || category.Url.Length == 0)
                return BadRequest(new ServiceResponse<Category>() { Message = "Please full fill form" });
            var result = await _categoryService.CreateCategory(category);
            return Ok(result);
        }

        [HttpPut, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<Category>>> UpdateCategory(Category category)
        {
            if (category == null || category.Name.Length == 0 || category.Url.Length == 0)
                return BadRequest(new ServiceResponse<Category>() { Message = "Please full fill form" });
            var result = await _categoryService.UpdateCategory(category);
            return Ok(result);
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategory(id);
            return Ok(result);
        }
    }
}
