using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                return context.Resources
                    .OrderBy(i => i.Name)
                    .ToList();
            }
        }

        [HttpPost(Name = "AddResourceContributor")]
        public async Task<ActionResult> PostResourceContributor(AddResourceContributorRequest request)
        {
            using (var context = new TradeUpContext())
            {
                var resource = await context.Resources
                    .Where(x => x.Id == request.ResourceId)
                    .FirstOrDefaultAsync();

                if (resource == null)
                {
                    return NotFound();
                }

                var contributor = await context.Contributors
                    .Where(x => x.Id == request.ContributorId)
                    .FirstOrDefaultAsync();

                if (contributor == null)
                {
                    return NotFound();
                }

                await context.ResourceContributors.AddAsync(new ResourceContributor()
                {
                    Resource = resource,
                    Contributor = contributor,
                    Frequency = request.Frequency,
                    MinContributionRange = request.MinContributionRange,
                    MaxContributionRange = request.MaxContributionRange,
                });

                await context.SaveChangesAsync();
                return Ok();
            }
        }

        public class AddResourceContributorRequest
        {
            public int ResourceId { get; set; }
            public int ContributorId { get; set; }
            public Frequency Frequency { get; set; }
            public int MinContributionRange { get; set; }
            public int MaxContributionRange { get; set; }
        }
    }
}