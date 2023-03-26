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
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
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
            if (response.Success)
            {
                //await _authService.SendEmail(user);
                await _userService.SetVerifiedEmail(request.Email, request.Password);
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("check-authen")]
        public async Task<ActionResult<ServiceResponse<string>>> CheckAuthen()
        {
            var response = await _authService.CheckAuthen();
            return response.Success ? (ActionResult<ServiceResponse<string>>)Ok(response) : (ActionResult<ServiceResponse<string>>)StatusCode(((int)HttpStatusCode.Unauthorized), response);
        }

        [HttpPost("change-password"), Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> ChangePassword(UserChangePassword userChangePassword)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _authService.ChangePassword(int.Parse(userId), userChangePassword.OldPassword, userChangePassword.NewPassword);

            return !response.Success ? (ActionResult<ServiceResponse<bool>>)BadRequest(response) : (ActionResult<ServiceResponse<bool>>)Ok(response);
        }
    }
}
