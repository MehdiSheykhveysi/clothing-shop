using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities;

namespace Shop.Dal.EF{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o=>o.ID);
            builder.Property(o=>o.Name).HasMaxLength(100);
            builder.Property(o=>o.Address).HasMaxLength(200);
            builder.Property(o=>o.City).HasMaxLength(100);
            builder.Property(o=>o.State).HasMaxLength(100);
            builder.Property(o=>o.Phone).HasMaxLength(20);
            builder.Property(o=>o.Country).HasMaxLength(100);
            builder.Property(o=>o.PeymentId).HasMaxLength(100);
        }
    }
}