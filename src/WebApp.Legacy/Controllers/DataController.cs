using Microsoft.AspNetCore.Mvc;
using WebApp.Legacy.Model;

namespace WebApp.Legacy.Controllers
{
    [Route("legacyWebapp")]
    [ApiController]
    public class DataController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new DataModel { Source = "LegacyWebApp", Data = "Legacy WebApp .NET Core 3.1" });
        }
    }
}
