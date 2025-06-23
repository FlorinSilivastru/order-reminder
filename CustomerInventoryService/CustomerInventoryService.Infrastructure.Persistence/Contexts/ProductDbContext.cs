using CustomerInventoryService.Domain.Models;
using CustomerInventoryService.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CustomerInventoryService.Infrastructure.Persistence.Contexts;

public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductEntityConfiguration).Assembly);
    }
}
