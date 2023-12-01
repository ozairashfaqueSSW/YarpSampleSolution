using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Route("webapp")]
[ApiController]
public class DataController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { source = "WebApp", data = "WebApp .NET 8 Data" });
    }
}
