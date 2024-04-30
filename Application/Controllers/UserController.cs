using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("GetUserById")]
        public async Task<ActionResult<User>> GetUserById(string userId)
        {
            var user = await _userService.GetUserById(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            var createdUser = await _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetUserById), new { userId = createdUser.UserId }, createdUser);
        }

        [HttpPut("UpdateUser")]
        public async Task<ActionResult<User>> UpdateUser(string userId, User user)
        {
            if (userId != user.UserId)
            {
                return BadRequest("User ID mismatch");
            }

            var updatedUser = await _userService.UpdateUser(user);
            if (updatedUser == null)
            {
                return NotFound();
            }

            return Ok(updatedUser);
        }

        [HttpDelete("DeleteUser")]
        public async Task<ActionResult> DeleteUser(string userId)
        {
            await _userService.DeleteUser(userId);
            return NoContent();
        }
    }
}