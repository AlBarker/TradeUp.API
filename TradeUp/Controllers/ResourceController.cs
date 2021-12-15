using Microsoft.AspNetCore.Mvc;
using TradeUp.Domain;
using TradeUp.Core.Requests;
using TradeUp.Core.Services;

namespace TradeUp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class ResourceController : ControllerBase
    {
        private readonly ILogger<ResourceController> _logger;
        private readonly IResourceService _resourceService;
        private readonly IResourceContributorService _resourceContributorService;

        public ResourceController(ILogger<ResourceController> logger, IResourceService resourceService, IResourceContributorService resourceContributorService)
        {
            _logger = logger;
            _resourceService = resourceService;
            _resourceContributorService = resourceContributorService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<Resource>> Get()
        {
            return await _resourceService.GetResources();
        }

        [HttpPost(Name = "AddResourceContributor")]
        public async Task<ActionResult> PostResourceContributor(AddResourceContributorRequest request)
        {
            return await _resourceContributorService.AddResourceContributor(request);
        }
    }
}