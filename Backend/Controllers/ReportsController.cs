using Brewery_SCADA_System.DTO;
using Brewery_SCADA_System.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using Brewery_SCADA_System.Models;

namespace Brewery_SCADA_System.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ReportsController : ControllerBase
    {
        private readonly IAlarmService _alarmService;
        private readonly ITagService _tagService;

        public ReportsController(IAlarmService alarmService, ITagService tagService)
        {
            _alarmService = alarmService;
            _tagService = tagService;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> allAlarmsByTime([FromQuery] DateTime timeFrom, [FromQuery] DateTime timeTo)
        {
            AuthenticateResult result = await HttpContext.AuthenticateAsync();
            if (result.Succeeded)
            {
                ClaimsIdentity identity = result.Principal.Identity as ClaimsIdentity;
                String userId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;

                List<AlarmReportsDTO> alarms =  await _alarmService.getAllAlarmsByTime(Guid.Parse(userId), timeFrom, timeTo);

                return Ok(alarms);
            }
            else
            {
                return Forbid("Authentication error!");
            }

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> allAlarmsByPriority([FromQuery] AlarmPriority alarmPriority)
        {
            AuthenticateResult result = await HttpContext.AuthenticateAsync();
            if (result.Succeeded)
            {
                ClaimsIdentity identity = result.Principal.Identity as ClaimsIdentity;
                String userId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;

                List<AlarmReportsDTO> alarms = await _alarmService.getAllAlarmsByPriority(Guid.Parse(userId), alarmPriority);

                return Ok(alarms);
            }
            else
            {
                return Forbid("Authentication error!");
            }

        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> allTagValuesByTime([FromQuery] DateTime timeFrom, [FromQuery] DateTime timeTo)
        {
            AuthenticateResult result = await HttpContext.AuthenticateAsync();
            if (result.Succeeded)
            {
                ClaimsIdentity identity = result.Principal.Identity as ClaimsIdentity;
                String userId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;

                TagReportsDto tagReports = await _tagService.getAllTagValuesByTime(Guid.Parse(userId), timeFrom, timeTo);

                return Ok(tagReports);
            }
            else
            {
                return Forbid("Authentication error!");
            }

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> latestAnalogValues()
        {
            AuthenticateResult result = await HttpContext.AuthenticateAsync();
            if (result.Succeeded)
            {
                ClaimsIdentity identity = result.Principal.Identity as ClaimsIdentity;
                String userId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;

                List<IOAnalogData> tagReports = await _tagService.getLatestAnalogTagsValues(Guid.Parse(userId));

                return Ok(tagReports);
            }
            else
            {
                return Forbid("Authentication error!");
            }

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> latestDigitalValues()
        {
            AuthenticateResult result = await HttpContext.AuthenticateAsync();
            if (result.Succeeded)
            {
                ClaimsIdentity identity = result.Principal.Identity as ClaimsIdentity;
                String userId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;

                List<IODigitalData> tagReports = await _tagService.getLatestDigitalTagsValues(Guid.Parse(userId));

                return Ok(tagReports);
            }
            else
            {
                return Forbid("Authentication error!");
            }

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> allAnalogTagValues([FromQuery] Guid tagId)
        {
            AuthenticateResult result = await HttpContext.AuthenticateAsync();
            if (result.Succeeded)
            {
                ClaimsIdentity identity = result.Principal.Identity as ClaimsIdentity;
                String userId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;

                List<IOAnalogData> tagReports = await _tagService.getAllAnalogTagValues(Guid.Parse(userId), tagId);

                return Ok(tagReports);
            }
            else
            {
                return Forbid("Authentication error!");
            }

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> allDigitalTagValues([FromQuery] Guid tagId)
        {
            AuthenticateResult result = await HttpContext.AuthenticateAsync();
            if (result.Succeeded)
            {
                ClaimsIdentity identity = result.Principal.Identity as ClaimsIdentity;
                String userId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;

                List<IODigitalData> tagReports = await _tagService.getAllDigitalTagValues(Guid.Parse(userId), tagId);

                return Ok(tagReports);
            }
            else
            {
                return Forbid("Authentication error!");
            }

        }
    }
}
