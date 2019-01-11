using Microsoft.EntityFrameworkCore;
using Shop.Dal.EF.Configurations;
using Shop.Domain.Entities;

namespace Shop.Dal.EF {
    public class ShopCenterDBContext : DbContext {
        public ShopCenterDBContext (DbContextOptions<ShopCenterDBContext> options) : base (options) {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        protected override void OnModelCreating (ModelBuilder modelBuilder) {

            modelBuilder.ApplyConfiguration(new ProductConfig());
            modelBuilder.ApplyConfiguration(new OrderConfig());
            base.OnModelCreating (modelBuilder);
        }
    }
}