﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TradeUp.Core.Requests;
using TradeUp.Domain;

namespace TradeUp.Core.Services
{
    public interface IResourceService
    {
        public Task<IList<Resource>> GetResources();
        public Task<ActionResult> AddResource(AddResourceRequest request);
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

        public async Task<ActionResult> AddResource(AddResourceRequest request)
        {
            using var context = new TradeUpContext();
            await context.Resources.AddAsync(new Resource()
            {
                Name = request.Name,
                CountAvailable = request.AvailableAmount,
                Price = request.Price
            });

            await context.SaveChangesAsync();

            return new OkResult();
        }
    }
}
