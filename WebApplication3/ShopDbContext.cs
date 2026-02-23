using Microsoft.EntityFrameworkCore;
using ShopAPI.Models;

namespace ShopAPI
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductsInOrders> ProductsInOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductsInOrders>()
                .HasKey(p => new { p.OrderId, p.ProductId });

            modelBuilder.Entity<ProductsInOrders>()
                .HasOne(p => p.Order)
                .WithMany(o => o.ProductsInOrders)
                .HasForeignKey(p => p.OrderId);

            modelBuilder.Entity<ProductsInOrders>()
                .HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(p => p.ProductId);
        }
    }
}
