using Microsoft.AspNetCore.Mvc;

namespace Yarp.Gateway.Controllers;

[Route("webapp")]
[ApiController]
public class ApplicationInformationController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { source = "WebApp .NET 8", data = "WebApp .NET 8 Data" });
    }
}
