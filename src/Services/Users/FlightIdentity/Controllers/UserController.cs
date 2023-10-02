using FlightIdentity.Models;
using FlightIdentity.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightIdentity.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] ApplicationUser user, string password)
        {
            var result = await _userService.CreateUserAsync(user, password);

            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(GetUserById), new { userId = user.Id }, user);
            }

            return BadRequest(result.Errors);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] ApplicationUser user)
        {
            if (userId != user.Id)
            {
                return BadRequest("User ID in the request body does not match the route parameter.");
            }

            var result = await _userService.UpdateUserAsync(user);

            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest(result.Errors);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userService.DeleteUserAsync(user);

            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest(result.Errors);
        }

    }
}
