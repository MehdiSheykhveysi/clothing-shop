using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities;

namespace Shop.Dal.EF.Configurations
{
    public class ProductConfig :IEntityTypeConfiguration<Product>
    {
       
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p=>p.PName).HasMaxLength(50).IsRequired();
            builder.Property(p=>p.Category).HasMaxLength(100).IsRequired();
            builder.Property(p=>p.Description).HasMaxLength(500);
            builder.Property(P=>P.Price).HasColumnType("decimal(18,2)");
        }
    }
}
