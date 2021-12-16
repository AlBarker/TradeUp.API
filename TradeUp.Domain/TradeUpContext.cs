using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TradeUp.Domain
{
    public class TradeUpContext : DbContext
    {
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Contributor> Contributors { get; set; }
        public DbSet<ResourceContributor> ResourceContributors { get; set; }
        public DbSet<ResourceContributionHistory> ResourceContributionHistory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var connectionString = "server=localhost;database=tradeup;user=root;password=admin";
            optionsBuilder.UseMySql(
                connectionString, ServerVersion.AutoDetect(connectionString))
                // The following three options help with debugging, but should
                // be changed or removed for production.
                //.LogTo(Console.WriteLine, LogLevel.Information)
                //.EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var iron = new Resource { Id = 1, Name = "Iron", Price = 10.5, CountAvailable = 100 };
            var copper = new Resource { Id = 2, Name = "Copper", Price = 4.0, CountAvailable = 50 };

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.HasData(
                    iron,
                    copper);
            });

            var london = new Contributor { Id = 1, Name = "London", ContributionFactor = 1, Size = 100 };
            var manchester = new Contributor { Id = 2, Name = "Manchester", ContributionFactor = 1, Size = 50 };

            modelBuilder.Entity<Contributor>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.HasData(
                   london,
                    manchester);
            });
        }
    }
}
