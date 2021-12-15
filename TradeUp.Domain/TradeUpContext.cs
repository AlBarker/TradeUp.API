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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var connectionString = "server=localhost;database=tradeup;user=root;password=admin";
            optionsBuilder.UseMySql(
                connectionString, ServerVersion.AutoDetect(connectionString))
                // The following three options help with debugging, but should
                // be changed or removed for production.
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.HasData(
                    new Resource { Id = 1, Name = "Iron", Price = 10.5, CountAvailable = 100 },
                    new Resource { Id = 2, Name = "Copper", Price = 4.0, CountAvailable = 50 });
            });
        }
    }
}
