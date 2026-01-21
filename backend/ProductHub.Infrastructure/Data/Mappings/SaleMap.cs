using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductHub.Domain.Sales;

namespace ProductHub.Infrastructure.Data.Mappings;

public class SaleMap : IEntityTypeConfiguration<Sale>
{
  public void Configure(EntityTypeBuilder<Sale> builder)
  {
    builder.ToTable("Sales");

    builder.HasKey(s => s.Id);

    builder.Property(s => s.ProductId)
        .IsRequired();

    builder.Property(s => s.Quantity)
        .IsRequired();

    builder.Property(s => s.SaleDate)
        .IsRequired();
  }
}
