using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TradeUp.Core.Requests;
using TradeUp.Domain;

namespace TradeUp.Core.Services
{
    public interface IConsumerService
    {
        Task<IList<Consumer>> GetConsumers();
        Task<ActionResult> AddConsumer(AddConsumerRequest request);
        Task ProcessConsumption(ResourceConsumer consumer, int day);

    }

    public class ConsumerService : IConsumerService
    {
        public async Task<IList<Consumer>> GetConsumers()
        {
            using var context = new TradeUpContext();

            return await context.Consumers.ToListAsync();
        }

        public async Task<ActionResult> AddConsumer(AddConsumerRequest request)
        {
            using var context = new TradeUpContext();

            await context.Consumers.AddAsync(new Consumer
            {
                Name = request.Name,
                Size = request.Size,
                ConsumptionFactor = request.ConsumptionFactor,
            });

            await context.SaveChangesAsync();

            return new OkResult();
        }

        public Task ProcessConsumption(ResourceConsumer consumer, int day)
        {
            throw new NotImplementedException();
        }
    }
}
