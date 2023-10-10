using Microsoft.AspNetCore.Mvc;
using FlightIdentity.DTOs;
using FlightIdentity.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace FlightIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountApiController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountApiController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // You may generate and return a token here for authentication, if needed.
                    return Ok("Registration successful");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return BadRequest(ModelState);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // You can generate and return a token here for authentication, if needed.
                    return Ok("Login successful");
                }

                if (result.IsLockedOut)
                {
                    // Handle account lockout logic if necessary
                    return BadRequest("Account locked out");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt");
                    return Unauthorized(ModelState);
                }
            }

            return BadRequest(ModelState);
        }

    }
}
