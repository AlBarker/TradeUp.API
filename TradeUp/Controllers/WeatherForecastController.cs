using Microsoft.AspNetCore.Mvc;
using TradeUp.Domain;

namespace TradeUp.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<Resource> Get()
        {
            using (var context = new TradeUpContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.SaveChanges();

                return context.Resources
                    .OrderBy(i => i.Name)
                    .ToList();
            }
        }
    }
}