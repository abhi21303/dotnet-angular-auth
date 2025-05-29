using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DotNetWebApi.Entities;
using Microsoft.AspNetCore.Identity;
namespace DotNetWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Created_DateTime = DateTime.UtcNow
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return Ok(new { Message = "User registered successfully!" });
            }
            return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid credentials." });
            }
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, true);
            if (result.Succeeded)
            {
                return Ok(new { Message = "Login successful!" });
            }
            else if (result.IsLockedOut)
            {
                return BadRequest(new { Message = "User account is locked." });
            }
            else if (result.IsNotAllowed)
            {
                return BadRequest(new { Message = "User is not allowed to login." });
            }
            return Unauthorized(new { Message = "Invalid login attempt." });
        }
    }
}
