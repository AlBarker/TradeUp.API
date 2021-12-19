using TradeUp.Core.Requests;
using TradeUp.Domain;

namespace TradeUp.Core.Services
{
    public interface IConsumerService
    {
        Task AddConsumer(AddConsumerRequest request);
        Task ProcessConsumption(ResourceConsumer consumer, int day);

    }

    public class ConsumerService : IConsumerService
    {
        public async Task AddConsumer(AddConsumerRequest request)
        {
            using var context = new TradeUpContext();

            await context.Consumers.AddAsync(new Consumer
            {
                Name = request.Name,
                Size = request.Size,
                ConsumptionFactor = request.ConsumptionFactor,
            });

            await context.SaveChangesAsync();
        }

        public Task ProcessConsumption(ResourceConsumer consumer, int day)
        {
            throw new NotImplementedException();
        }
    }
}
