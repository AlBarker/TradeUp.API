using Microsoft.AspNetCore.Mvc;
using TradeUp.Domain;
using TradeUp.Core.Requests;
using TradeUp.Core.Services;
using TradeUp.API.HubConfig;
using Microsoft.AspNetCore.SignalR;

namespace TradeUp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public partial class ResourceController : ControllerBase
    {
        private readonly ILogger<ResourceController> _logger;
        private readonly IResourceService _resourceService;
        private readonly IHubContext<ResourceHub> _hub;

        public ResourceController(ILogger<ResourceController> logger, IResourceService resourceService, IHubContext<ResourceHub> hub)
        {
            _logger = logger;
            _resourceService = resourceService;
            _hub = hub;
        }

        //[HttpGet(Name = "GetResources")]
        //public async Task<IEnumerable<Resource>> Get()
        //{
        //    return await _resourceService.GetResources();
        //}

        [HttpGet(Name = "GetResourcesSignalR")]
        public IActionResult Get()
        {
            var timerManager = new TimerManager(() => _hub.Clients.All.SendAsync("transferresourcedata", _resourceService.GetResources()));
            return Ok(new { Message = "Request Completed" });
        }

        [HttpPost(Name = "AddResource")]
        public async Task<ActionResult> PostResource(AddResourceRequest request)
        {
            return await _resourceService.AddResource(request);
        }
    }
}