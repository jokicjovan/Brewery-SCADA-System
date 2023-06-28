using Microsoft.AspNetCore.Mvc;

namespace Brewery_SCADA_System.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class Test : ControllerBase
    {
        public Test()
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            return Ok(0);
        }
    }
}