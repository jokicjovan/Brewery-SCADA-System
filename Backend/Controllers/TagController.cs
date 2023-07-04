using Brewery_SCADA_System.DTO;
using Brewery_SCADA_System.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace Brewery_SCADA_System.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TagController : ControllerBase
    {
        private readonly IDeviceService _deviceService;
        private readonly ITagService _tagService;
        public TagController(IDeviceService deviceService, ITagService tagService)
        {
            _deviceService = deviceService;
            _tagService = tagService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> addAnalogInput([FromBody] AnalogInputDTO analogInputDTO)
        {
            AuthenticateResult result = await HttpContext.AuthenticateAsync();
            if (result.Succeeded)
            {
                ClaimsIdentity identity = result.Principal.Identity as ClaimsIdentity;
                String userId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                await _tagService.addAnalogInputAsync(new Models.AnalogInput(analogInputDTO),Guid.Parse(userId));
                return Ok();
            }
            else
            {
                return Forbid("Authentication error!");
            }
            
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> addDigitalInput([FromBody] DigitalInputDTO digitalInputDTO)
        {
            AuthenticateResult result = await HttpContext.AuthenticateAsync();
            if (result.Succeeded)
            {
                ClaimsIdentity identity = result.Principal.Identity as ClaimsIdentity;
                String userId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                await _tagService.addDigitalInputAsync(new Models.DigitalInput(digitalInputDTO), Guid.Parse(userId));
                return Ok();
            }
            else
            {
                return Forbid("Authentication error!");
            }

        }

    }
}
