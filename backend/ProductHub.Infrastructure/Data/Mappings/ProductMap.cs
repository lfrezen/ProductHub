using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductHub.Domain.Products;

namespace ProductHub.Infrastructure.Data.Mappings;

public class ProductMap : IEntityTypeConfiguration<Product>
{
  public void Configure(EntityTypeBuilder<Product> builder)
  {
    builder.ToTable("Products");

    builder.HasKey(p => p.Id);

    builder.Property(p => p.Name)
        .IsRequired()
        .HasMaxLength(200);

    builder.Property(p => p.Price)
        .HasPrecision(18, 2);

    builder.Property(p => p.Quantity)
        .IsRequired();

    builder.Property(p => p.IsOutOfStock)
        .IsRequired();

    builder.Property(p => p.LastSaleDate);
  }
}
