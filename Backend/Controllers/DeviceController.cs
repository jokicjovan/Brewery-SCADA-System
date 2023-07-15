using Brewery_SCADA_System.DTO;
using Brewery_SCADA_System.Models;
using Brewery_SCADA_System.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Brewery_SCADA_System.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;
        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpGet]
        public async Task<ActionResult> startSimulation()
        {
            _deviceService.StartSimulation();
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult> getAllAddresses()
        {
            return Ok(_deviceService.GetAllAddresses());
        }

    }
}
