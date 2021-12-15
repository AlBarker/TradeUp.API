using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TradeUp.Core.Requests;
using TradeUp.Domain;

namespace TradeUp.Core.Services
{
    public interface IResourceContributorService
    {
        Task<ActionResult> AddResourceContributor(AddResourceContributorRequest request);
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
    }
}
