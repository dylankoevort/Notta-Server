using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Application.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }
    
    // GET: api/User/GetUsers
    [HttpGet("GetUsers")]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        var users = _userService.GetAllUsers();
        return Ok(users);
    }
    
    // GET: api/User/GetUserById/{userId/userUid
    [HttpGet("GetUserById")]
    public ActionResult<User> GetUserById(int? userId, string? userUid)
    {

        if (!userId.HasValue && string.IsNullOrEmpty(userUid))
        {
            return BadRequest("userId or userUid must be provided.");
        }

        User user = null;

        if (userId.HasValue && userId != 0)
        {
            user = _userService.GetUserById(userId.Value);
        }
        else if (!string.IsNullOrEmpty(userUid))
        {
            user = _userService.GetUserById(userUid);
        }

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }
    
    // POST: api/User/AddUser
    [HttpPost("AddUser")]
    public ActionResult<User> AddUser([FromBody] User user)
    {
        if (user == null)
        {
            return BadRequest();
        }

        _userService.AddUser(user);

        return CreatedAtAction(nameof(GetUserById), new { userId = user.UserId }, user);
    }
    
    // PUT: api/User/UpdateUser/{userId}
    [HttpPut("UpdateUser")]
    public IActionResult UpdateUser(string userUid, [FromBody] User user)
    {
        if (!userUid.Equals(user.UserUid))
        {
            return BadRequest();
        }

        _userService.UpdateUser(user);

        return Ok();
    }
    
    // DELETE: api/User/DeleteUser/{userId}
    [HttpDelete("DeleteUser")]
    public IActionResult DeleteUser(int? userId, string? userUid)
    {
        if (!userId.HasValue && string.IsNullOrEmpty(userUid))
        {
            return BadRequest("userId or userUid must be provided.");
        }

        if (userId.HasValue && userId != 0)
        {
            var user = _userService.GetUserById(userId.Value);
            if (user == null)
            {
                return NotFound();
            }

            _userService.DeleteUser(userId.Value);
        }
        else if (!string.IsNullOrEmpty(userUid))
        {
            var user = _userService.GetUserById(userUid);
            if (user == null)
            {
                return NotFound();
            }

            _userService.DeleteUser(user.UserId);
        }

        return Ok();
    }
}