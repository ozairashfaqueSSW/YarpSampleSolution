using Microsoft.AspNetCore.Mvc;
using Yarp.Gateway.Model;

namespace Yarp.Gateway.Controllers.api;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();

        return Ok((new DataModel
        {
            Data = String.Join(Environment.NewLine, forecasts.Select(forecast =>
                $"{forecast.Date}  {forecast.TemperatureC}°C / {forecast.TemperatureF}°F {forecast.Summary}")),
            Source = "WebApp .NET 8"
        }));
    }

}
