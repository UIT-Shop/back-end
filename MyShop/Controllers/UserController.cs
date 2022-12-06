using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet(), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<User>>>> GetUsers()
        {
            var result = await _userService.GetUsers();
            return !result.Success ? (ActionResult<ServiceResponse<List<User>>>)BadRequest(result) : (ActionResult<ServiceResponse<List<User>>>)Ok(result);
        }


        [HttpGet("{userId}")]
        public async Task<ActionResult<ServiceResponse<List<User>>>> GetUserInfo(int userId)
        {
            var result = await _userService.GetUserInfo(userId);
            return !result.Success ? (ActionResult<ServiceResponse<List<User>>>)BadRequest(result) : (ActionResult<ServiceResponse<List<User>>>)Ok(result);
        }

        [HttpPut()]
        public async Task<ActionResult<ServiceResponse<List<User>>>> UpdateUser(User user)
        {
            var result = await _userService.UpdateUser(user);
            return !result.Success ? (ActionResult<ServiceResponse<List<User>>>)BadRequest(result) : (ActionResult<ServiceResponse<List<User>>>)Ok(result);
        }

        [HttpPut("{userId}/role/{role}")]
        public async Task<ActionResult<ServiceResponse<List<User>>>> ChangeRole(int userId, Role role)
        {
            var result = await _userService.ChangeRole(userId, role);
            return !result.Success ? (ActionResult<ServiceResponse<List<User>>>)BadRequest(result) : (ActionResult<ServiceResponse<List<User>>>)Ok(result);
        }

        [HttpDelete("{userId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<User>>>> DeleteUser(int userId)
        {
            var result = await _userService.DeleteUser(userId);
            return !result.Success ? (ActionResult<ServiceResponse<List<User>>>)BadRequest(result) : (ActionResult<ServiceResponse<List<User>>>)Ok(result);
        }
    }
}
