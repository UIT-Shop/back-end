using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Color>>>> GetColors()
        {
            var response = await _colorService.GetColors();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Color>>> GetColor(int id)
        {
            var response = await _colorService.GetColor(id);
            return Ok(response);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<Color>>>> AddColor(Color color)
        {
            var response = await _colorService.AddColor(color);
            return Ok(response);
        }

        [HttpPut, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<Color>>>> UpdateColor(Color color)
        {
            var response = await _colorService.UpdateColor(color);
            return Ok(response);
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteColor(int id)
        {
            var result = await _colorService.DeleteColor(id);
            return Ok(result);
        }
    }
}
