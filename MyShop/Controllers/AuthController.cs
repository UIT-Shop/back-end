using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegister request)
        {
            var response = await _authService.Register(
                new User
                {
                    Email = request.Email,
                    Name = request.Name ?? "",
                },
                request.Password);

            return !response.Success ? (ActionResult<ServiceResponse<int>>)BadRequest(response) : (ActionResult<ServiceResponse<int>>)Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLogin request)
        {
            var response = await _authService.Login(request.Email, request.Password);
            return !response.Success ? (ActionResult<ServiceResponse<string>>)BadRequest(response) : (ActionResult<ServiceResponse<string>>)Ok(response);
        }

        [HttpGet("check-authen")]
        public async Task<ActionResult<ServiceResponse<string>>> CheckAuthen()
        {
            var response = await _authService.CheckAuthen();
            return response.Success ? (ActionResult<ServiceResponse<string>>)Ok(response) : (ActionResult<ServiceResponse<string>>)StatusCode(((int)HttpStatusCode.Forbidden), response);
        }

        [HttpPost("change-password"), Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> ChangePassword(UserChangePassword userChangePassword)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _authService.ChangePassword(int.Parse(userId), userChangePassword.Password);

            return !response.Success ? (ActionResult<ServiceResponse<bool>>)BadRequest(response) : (ActionResult<ServiceResponse<bool>>)Ok(response);
        }
    }
}
