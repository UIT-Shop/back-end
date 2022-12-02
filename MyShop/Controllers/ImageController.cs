using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Image>>>> GetImages()
        {
            var result = await _imageService.GetImages();
            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<ActionResult<ServiceResponse<Image>>> GetImage(int id)
        {
            var response = await _imageService.GetImage(id);
            return !response.Success ? (ActionResult<ServiceResponse<Image>>)BadRequest(response) : (ActionResult<ServiceResponse<Image>>)Ok(response);

        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<Product>>> CreateImage(Image image)
        {
            var result = await _imageService.CreateImage(image);
            return Ok(result);
        }

        [HttpPut, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<Product>>> UpdateImage(Image image)
        {
            var result = await _imageService.UpdateImage(image);
            return Ok(result);
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteImage(int id)
        {
            var result = await _imageService.DeleteImage(id);
            return Ok(result);
        }
    }
}
