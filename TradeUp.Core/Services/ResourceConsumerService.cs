using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TradeUp.Core.Requests;
using TradeUp.Domain;

namespace TradeUp.Core.Services
{
    public interface IResourceConsumerService
    {
        Task<ActionResult> AddResourceConsumer(AddResourceConsumerRequest request);
        Task<IList<ResourceConsumer>> GetResourceConsumers();
        Task ProcessConsumption(ResourceConsumer toProcess, int dayCount);
    }
    public class ResourceConsumerService : IResourceConsumerService
    {
        public async Task<ActionResult> AddResourceConsumer(AddResourceConsumerRequest request)
        {
            using (var context = new TradeUpContext())
            {
                var resource = await context.Resources
                    .Where(x => x.Id == request.ResourceId)
                    .FirstOrDefaultAsync();

                if (resource == null)
                {
                    return new NotFoundResult();
                }

                var consumer = await context.Consumers
                    .Where(x => x.Id == request.ConsumerId)
                    .FirstOrDefaultAsync();
                 
                if (consumer == null)
                {
                    return new NotFoundResult();
                }

                await context.ResourceConsumers.AddAsync(new ResourceConsumer()
                {
                    Resource = resource,
                    Consumer = consumer,
                    Frequency = request.Frequency,
                    MinConsumptionRange = request.MinConsumptionRange,
                    MaxConsumptionRange = request.MaxConsumptionRange,
                });

                await context.SaveChangesAsync();
                return new OkResult();

            }
        }

        public async Task<IList<ResourceConsumer>> GetResourceConsumers()
        {
            using var context = new TradeUpContext();

            return await context.ResourceConsumers
                .Include(x => x.Resource)
                .Include(x => x.Consumer)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task ProcessConsumption(ResourceConsumer toProcess, int dayCount)
        {
            using var context = new TradeUpContext();

            var trackedResouceConsumer = await context.ResourceConsumers
                .Where(x => x.Id == toProcess.Id)
                .Include(x => x.Consumer)
                .Include(x => x.Resource)
                .FirstOrDefaultAsync();

            if(trackedResouceConsumer == null)
            {
                throw new ArgumentException("Invalid ResourceContributor passed in to ProcessContribution");
            }

            Random rand = new Random();
            var resourcesToConsume = rand.Next(trackedResouceConsumer.MinConsumptionRange, trackedResouceConsumer.MaxConsumptionRange) * trackedResouceConsumer.Consumer.ConsumptionFactor;

            trackedResouceConsumer.Resource.CountAvailable -= resourcesToConsume;

            await context.ResourceConsumptionHistory.AddAsync(new ResourceConsumptionHistory()
            {
                ResourceConsumer = trackedResouceConsumer,
                Day = dayCount,
                ResourcePriceAtTimeOfConsumption = trackedResouceConsumer.Resource.Price,
                ResourcesConsumed = resourcesToConsume,
                ResourceCountAfterConsumption = trackedResouceConsumer.Resource.CountAvailable,
            });

            await context.SaveChangesAsync();
        }
    }
}