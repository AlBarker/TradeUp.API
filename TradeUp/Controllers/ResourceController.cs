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

        public ResourceController(ILogger<ResourceController> logger, IResourceService resourceService)
        {
            _logger = logger;
            _resourceService = resourceService;
        }

        [HttpGet(Name = "GetResources")]
        public async Task<IEnumerable<Resource>> Get()
        {
            return await _resourceService.GetResources();
        }

        [HttpPost(Name = "AddResource")]
        public async Task<ActionResult> PostResource(AddResourceRequest request)
        {
            return await _resourceService.AddResource(request);
        }
    }
}