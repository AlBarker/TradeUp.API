using Microsoft.AspNetCore.Mvc;
using TradeUp.Domain;
using TradeUp.Core.Requests;
using TradeUp.Core.Services;

namespace TradeUp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class ResourceContributorController : ControllerBase
    {
        private readonly ILogger<ResourceContributorController> _logger;
        private readonly IResourceContributorService _resourceContributorService;

        public ResourceContributorController(ILogger<ResourceContributorController> logger, IResourceContributorService resourceContributorService)
        {
            _logger = logger;
            _resourceContributorService = resourceContributorService;
        }

        [HttpPost(Name = "AddResourceContributor")]
        public async Task<ActionResult> PostResourceContributor(AddResourceContributorRequest request)
        {
            return await _resourceContributorService.AddResourceContributor(request);
        }
    }
}