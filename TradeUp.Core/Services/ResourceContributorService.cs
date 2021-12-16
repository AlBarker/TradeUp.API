using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TradeUp.Core.Requests;
using TradeUp.Domain;

namespace TradeUp.Core.Services
{
    public interface IResourceContributorService
    {
        Task<ActionResult> AddResourceContributor(AddResourceContributorRequest request);
        Task<IList<ResourceContributor>> GetResourceContributors();
        Task ProcessContribution(ResourceContributor toProcess, int dayCount);
    }
    public class ResourceContributorService : IResourceContributorService
    {
        public async Task<ActionResult> AddResourceContributor(AddResourceContributorRequest request)
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

                var contributor = await context.Contributors
                    .Where(x => x.Id == request.ContributorId)
                    .FirstOrDefaultAsync();
                 
                if (contributor == null)
                {
                    return new NotFoundResult();
                }

                await context.ResourceContributors.AddAsync(new ResourceContributor()
                {
                    Resource = resource,
                    Contributor = contributor,
                    Frequency = request.Frequency,
                    MinContributionRange = request.MinContributionRange,
                    MaxContributionRange = request.MaxContributionRange,
                });

                await context.SaveChangesAsync();
                return new OkResult();

            }
        }

        public async Task<IList<ResourceContributor>> GetResourceContributors()
        {
            using var context = new TradeUpContext();

            return await context.ResourceContributors
                .Include(x => x.Resource)
                .Include(x => x.Contributor)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task ProcessContribution(ResourceContributor toProcess, int dayCount)
        {
            using var context = new TradeUpContext();

            var trackedResouceContributor = await context.ResourceContributors
                .Where(x => x.Id == toProcess.Id)
                .Include(x => x.Contributor)
                .Include(x => x.Resource)
                .FirstOrDefaultAsync();

            if(trackedResouceContributor == null)
            {
                throw new ArgumentException("Invalid ResourceContributor passed in to ProcessContribution");
            }

            Random rand = new Random();
            var resourcesToAdd = rand.Next(trackedResouceContributor.MinContributionRange, trackedResouceContributor.MaxContributionRange) * trackedResouceContributor.Contributor.ContributionFactor;

            trackedResouceContributor.Resource.CountAvailable += resourcesToAdd;

            await context.ResourceContributionHistory.AddAsync(new ResourceContributionHistory()
            {
                ResourceContributor = trackedResouceContributor,
                Day = dayCount,
                ResourcePriceAtTimeOfContribution = trackedResouceContributor.Resource.Price,
                ResourceCount = trackedResouceContributor.Resource.CountAvailable,
            });

            await context.SaveChangesAsync();
        }
    }
}