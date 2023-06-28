using Brewery_SCADA_System.DTO;
using Brewery_SCADA_System.Services;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult register([FromBody] UserDTO userDto)
        {
            _userService.createUser(userDto);
            return Ok("Registration successful!");
        }
    }
}
