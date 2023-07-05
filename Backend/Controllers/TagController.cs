using Brewery_SCADA_System.DTO;
using Brewery_SCADA_System.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
    using System.Security.Cryptography.X509Certificates;
using Brewery_SCADA_System.Exceptions;
using Brewery_SCADA_System.Models;


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
                await _tagService.addAnalogInputAsync(new Models.AnalogInput(analogInputDTO), Guid.Parse(userId));
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


        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        public async Task<ActionResult> deleteDigitalInput(Guid id)
        {
            AuthenticateResult result = await HttpContext.AuthenticateAsync();
            if (result.Succeeded)
            {
                ClaimsIdentity identity = result.Principal.Identity as ClaimsIdentity;
                String userId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                await _tagService.deleteDigitalInputAsync(id, Guid.Parse(userId));
                return Ok();
            }
            else
            {
                return Forbid("Authentication error!");
            }

        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        public async Task<ActionResult> deleteAnalogInput(Guid id)
        {
            AuthenticateResult result = await HttpContext.AuthenticateAsync();
            if (result.Succeeded)
            {
                ClaimsIdentity identity = result.Principal.Identity as ClaimsIdentity;
                String userId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                await _tagService.deleteAnalogInputAsync(id, Guid.Parse(userId));
                return Ok();
            }
            else
            {
                return Forbid("Authentication error!");
            }

        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> switchTag([FromQuery] TagType type, [FromQuery] Guid tagId)
        {
            AuthenticateResult result = await HttpContext.AuthenticateAsync();
            if (result.Succeeded)
            {
                ClaimsIdentity identity = result.Principal.Identity as ClaimsIdentity;
                String userId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (type == TagType.ANALOG)
                    await _tagService.switchAnalogTag(tagId, Guid.Parse(userId));
                else if (type == TagType.DIGITAL)
                    await _tagService.switchDigitalTag(tagId, Guid.Parse(userId));
                else
                    throw new InvalidInputException("Invalid tag type");
                return Ok();
            }
            else
            {
                return Forbid("Authentication error!");
            }
        }
    }
}
