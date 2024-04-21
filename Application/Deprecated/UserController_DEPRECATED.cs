// using Infrastructure;
// using Microsoft.AspNetCore.Mvc;
// using Models;
//
// namespace Application.Controllers;
//
// [Route("api/[controller]")]
// [ApiController]
// public class UserController : ControllerBase
// {
//     private readonly IUserService _userService;
//
//     public UserController(IUserService userService)
//     {
//         _userService = userService ?? throw new ArgumentNullException(nameof(userService));
//     }
//     
// // GET: api/User/GetUsers
//     [HttpGet("GetUsers")]
//     public ActionResult<IEnumerable<User>> GetUsers()
//     {
//         try
//         {
//             var users = _userService.GetAllUsers();
//             return Ok(users);
//         }
//         catch (Exception ex)
//         {
//             // logger.LogError(ex, "An error occurred while getting users");
//             Console.WriteLine(ex.Message);
//             return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
//         }
//     }
//     
//     // GET: api/User/GetUserById/{userId/userUid}
//     [HttpGet("GetUserById")]
//     public ActionResult<User> GetUserById(int? userId, string? userUid)
//     {
//         try
//         {
//             if (userId == null && userUid == null)
//             {
//                 return BadRequest("userId or userUid must be provided");
//             }
//
//             User user = _userService.GetUserById(userId, userUid);
//         
//             if (user == null)
//             {
//                 return NotFound("User not found");
//             }
//
//             return Ok(user);
//         }
//         catch (Exception ex)
//         {
//             // logger.LogError(ex, "An error occurred while processing GetUserById request");
//             Console.WriteLine(ex.Message);
//             return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
//         }
//     }
//     
// // POST: api/User/AddUser
//     [HttpPost("AddUser")]
//     public ActionResult<User> AddUser([FromBody] User user)
//     {
//         try
//         {
//             if (user == null)
//             {
//                 return BadRequest("User data is missing");
//             }
//
//             _userService.AddUser(user);
//
//             return CreatedAtAction(nameof(GetUserById), new { userId = user.UserId }, user);
//         }
//         catch (Exception ex)
//         {
//             // logger.LogError(ex, "An error occurred while adding user");
//             Console.WriteLine(ex.Message);
//             return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
//         }
//     }
//     
// // PUT: api/User/UpdateUser/{userUid}
//     [HttpPut("UpdateUser")]
//     public IActionResult UpdateUser(string userUid, [FromBody] User user)
//     {
//         try
//         {
//             if (!userUid.Equals(user.UserUid))
//             {
//                 return BadRequest("User Uid mismatch");
//             }
//
//             _userService.UpdateUser(user);
//             return Ok("User updated successfully");
//         }
//         catch (Exception ex)
//         {
//             // logger.LogError(ex, "An error occurred while updating user");
//             Console.WriteLine(ex.Message);
//             return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
//         }
//     }
//
//     
// // DELETE: api/User/DeleteUser/{userId/userUid}
//     [HttpDelete("DeleteUser")]
//     public IActionResult DeleteUser(int? userId, string? userUid)
//     {
//         try
//         {
//             if (userId == null && userUid == null)
//             {
//                 return BadRequest("userId or userUid must be provided");
//             }
//
//             _userService.DeleteUser(userId, userUid);
//             return Ok("User deleted successfully");
//         }
//         catch (Exception ex)
//         {
//             // logger.LogError(ex, "An error occurred while deleting user");
//             Console.WriteLine(ex.Message);
//             return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
//         }
//     }
// }