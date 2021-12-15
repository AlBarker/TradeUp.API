using Microsoft.AspNetCore.Mvc;
using TradeUp.Domain;
using TradeUp.Core.Requests;
using TradeUp.Core.Services;

namespace TradeUp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class ResourceContributerController : ControllerBase
    {
        private readonly ILogger<ResourceContributerController> _logger;
        private readonly IResourceContributorService _resourceContributorService;

        public ResourceContributerController(ILogger<ResourceContributerController> logger, IResourceContributorService resourceContributorService)
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