using Microsoft.AspNetCore.Mvc;
using TradeUp.Domain;
using TradeUp.Core.Requests;
using TradeUp.Core.Services;

namespace TradeUp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class ResourceConsumerController : ControllerBase
    {
        private readonly ILogger<ResourceConsumerController> _logger;
        private readonly IResourceConsumerService _resourceConsumerService;

        public ResourceConsumerController(ILogger<ResourceConsumerController> logger, IResourceConsumerService resourceConsumerService)
        {
            _logger = logger;
            _resourceConsumerService = resourceConsumerService;
        }

        [HttpPost(Name = "AddResourceConsumer")]
        public async Task<ActionResult> PostResourceCOnsumer(AddResourceConsumerRequest request)
        {
            return await _resourceConsumerService.AddResourceConsumer(request);
        }
    }
}