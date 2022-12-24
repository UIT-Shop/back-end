using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost(), Authorize]
        public async Task<ActionResult<ServiceResponse<Comment>>> AddCommentAsync(Comment comment)
        {
            var result = await _commentService.AddComment(comment);
            return !result.Success ? (ActionResult<ServiceResponse<Comment>>)BadRequest(result) : (ActionResult<ServiceResponse<Comment>>)Ok(result);
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<ServiceResponse<List<Comment>>>> GetComments(int productId, [Required] int page)
        {
            var result = await _commentService.GetComments(productId, page);
            return Ok(result);
        }
    }
}
