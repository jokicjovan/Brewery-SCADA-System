using Brewery_SCADA_System.DTO;
using Brewery_SCADA_System.Models;
using Brewery_SCADA_System.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Xml;
using Newtonsoft.Json;

namespace Brewery_SCADA_System.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult> login([FromBody] UserLoginDTO userLoginDto)
        {
            User userToAuthenticate = new User();
            userToAuthenticate.Email = userLoginDto.Email;
            userToAuthenticate.Password = userLoginDto.Password;
            User authenticatedUser = await _userService.Authenticate(userToAuthenticate);

            ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Role, authenticatedUser.Discriminator));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, authenticatedUser.Id.ToString()));
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            return Ok("Logged in successfully!");
        }

        [HttpPost]
        [Authorize(Roles =("Admin"))]
        public async Task<ActionResult> register([FromBody] UserDTO userDto)
        {
            AuthenticateResult result = await HttpContext.AuthenticateAsync();
            if (result.Succeeded)
            {
                ClaimsIdentity identity = result.Principal.Identity as ClaimsIdentity;
                String userId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                User user = new User(userDto.Name, userDto.Surname, userDto.Email, userDto.Password, "User",await _userService.Get(Guid.Parse(userId)));
                _userService.CreateUser(user);
                return Ok("Registration successful!");
            }
            else
            {
                return Forbid("Authentication error!");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<string>> logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok("Logged out successfully!");
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult<string>> whoAmI()
        {
            AuthenticateResult result = await HttpContext.AuthenticateAsync();
            if (!result.Succeeded)
            {
                return BadRequest("Cookie error");
            }
            ClaimsIdentity identity = result.Principal.Identity as ClaimsIdentity;
            String role = identity.FindFirst(ClaimTypes.Role).Value;
            return Ok(JsonConvert.SerializeObject(new { role }, Newtonsoft.Json.Formatting.Indented));
        }
    }
}
