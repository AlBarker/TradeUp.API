using Microsoft.AspNetCore.Mvc;
using TradeUp.Domain;
using TradeUp.Core.Requests;
using TradeUp.Core.Services;

namespace TradeUp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class ConsumerController : ControllerBase
    {
        private readonly ILogger<ConsumerController> _logger;
        private readonly IConsumerService _consumerService;

        public ConsumerController(ILogger<ConsumerController> logger, IConsumerService consumerService)
        {
            _logger = logger;
            _consumerService = consumerService;
        }

        [HttpGet(Name = "GetConsumers")]
        public async Task<IList<Consumer>> Get()
        {
            return await _consumerService.GetConsumers();
        }

        [HttpPost(Name = "AddConsumers")]
        public async Task<ActionResult> PostResource(AddConsumerRequest request)
        {
            return await _consumerService.AddConsumer(request);
        }
    }
}