using Brewery_SCADA_System.DTO;
using Brewery_SCADA_System.Models;
using Brewery_SCADA_System.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using Brewery_SCADA_System.Exceptions;

namespace Brewery_SCADA_System.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AlarmController : ControllerBase
    {
        private readonly IAlarmService _alarmService;
        private readonly ITagService _tagService;

        public AlarmController(IAlarmService alarmService, ITagService tagService)
        {
            _alarmService = alarmService;
            _tagService = tagService;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> addAlarm([FromBody] AlarmDTO alarmDto)
        {
            AuthenticateResult result = await HttpContext.AuthenticateAsync();
            if (result.Succeeded)
            {
                ClaimsIdentity identity = result.Principal.Identity as ClaimsIdentity;
                String userId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;

                await  _alarmService.makeAlarm(Guid.Parse(userId), alarmDto);


                return Ok("Successfully created alarm!");
            }
            else
            {
                return Forbid("Authentication error!");
            }

        }


        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> deleteAlarm([FromQuery] Guid tagId, [FromQuery] Guid alarmId)
        {
            AuthenticateResult result = await HttpContext.AuthenticateAsync();
            if (result.Succeeded)
            {
                ClaimsIdentity identity = result.Principal.Identity as ClaimsIdentity;
                String userId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;

                await _alarmService.deleteAlarm(Guid.Parse(userId), alarmId, tagId);


                return Ok("Successfully removed alarm!");
            }
            else
            {
                return Forbid("Authentication error!");
            }

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> getByTag([FromQuery] Guid tagId)
        {
            AuthenticateResult result = await HttpContext.AuthenticateAsync();
            if (result.Succeeded)
            {
                ClaimsIdentity identity = result.Principal.Identity as ClaimsIdentity;
                String userId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                AnalogInput input= await _tagService.getAnalogInput(tagId, Guid.Parse(userId));
                return Ok(input.Alarms);
            }
            else
            {
                return Forbid("Authentication error!");
            }
        }
    }
}
