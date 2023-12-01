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
            return Ok(new DataModel { Source = "WebApp.Legacy", Data = "Legacy App Data" });
        }
    }
}
