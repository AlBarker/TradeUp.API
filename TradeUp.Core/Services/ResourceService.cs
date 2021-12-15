using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TradeUp.Domain;

namespace TradeUp.Core.Services
{
    public interface IResourceService
    {
        public Task<IList<Resource>> GetResources();
    }

    public class ResourceService : IResourceService
    {
        public ILogger<ResourceService> logger;
        public ResourceService(ILogger<ResourceService> logger)
        {
            this.logger = logger;
        }

        public async Task<IList<Resource>> GetResources()
        {
            using (var context = new TradeUpContext())
            {
                return await context.Resources
                    .OrderBy(i => i.Name)
                    .ToListAsync();
            }
        }
    }
}
