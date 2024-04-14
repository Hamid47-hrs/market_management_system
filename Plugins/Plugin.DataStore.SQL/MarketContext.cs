using market_management_system.Models;
using Microsoft.EntityFrameworkCore;

namespace market_management_system.Plugins.Plugin.DataStore.SQL;

public class MarketContext : DbContext
{
    public MarketContext(DbContextOptions options)
        : base(options) { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<Category>()
            .HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId);

        modelBuilder
            .Entity<Category>()
            .HasData(
                new Category
                {
                    Id = 1,
                    Name = "test1",
                    Description = "aLorem ipsum"
                },
                new Category
                {
                    Id = 2,
                    Name = "test2",
                    Description = "aLorem ipsum dolor"
                },
                new Category
                {
                    Id = 3,
                    Name = "test3",
                    Description = "aLorem ipsum dolor sit"
                },
                new Category
                {
                    Id = 4,
                    Name = "test4",
                    Description = "aLorem ipsum dolor sit amet"
                }
            );

        modelBuilder
            .Entity<Product>()
            .HasData(
                new Product
                {
                    Id = 1,
                    CategoryId = 1,
                    Name = "product1",
                    Price = 11.11,
                    Quantity = 2
                },
                new Product
                {
                    Id = 2,
                    CategoryId = 1,
                    Name = "product2",
                    Price = 22.11,
                    Quantity = 3
                },
                new Product
                {
                    Id = 3,
                    CategoryId = 2,
                    Name = "product3",
                    Price = 33.11,
                    Quantity = 6
                },
                new Product
                {
                    Id = 4,
                    CategoryId = 3,
                    Name = "product4",
                    Price = 44.11,
                    Quantity = 4
                }
            );
    }
}
