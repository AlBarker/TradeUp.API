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
        Task ProcessContribution(ResourceContributor toProcess);
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

            return await context.ResourceContributors.ToListAsync();
        }

        public async Task ProcessContribution(ResourceContributor toProcess)
        {
            using var context = new TradeUpContext();

            var resourceToModify = await context.Resources.Where(x => x.Id == toProcess.Resource.Id).FirstOrDefaultAsync();
            var contributor = await context.Contributors.Where(x => x.Id == toProcess.Contributor.Id).FirstOrDefaultAsync();

            if(contributor == null || resourceToModify == null)
            {
                throw new ArgumentException("Invalid Contributor or Resource passed in to ProcessContribution");
            }

            Random rand = new Random();
            var resourcesToAdd = rand.Next(toProcess.MinContributionRange, toProcess.MaxContributionRange) * contributor.ContributionFactor;

            resourceToModify.CountAvailable += resourcesToAdd;

            await context.SaveChangesAsync();
        }
    }
}