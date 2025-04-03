using DeveloperStore.Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeveloperStore.Sales.Infrastructure.Data.Mappings
{
    public sealed class SaleItemMap : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");

            builder.HasKey(item => item.Id);

            builder.Property(item => item.ProductId)
                .IsRequired();

            builder.Property(item => item.ProductName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(item => item.Quantity)
                .IsRequired();

            builder.Property(item => item.UnitPrice)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(item => item.Discount)
                .IsRequired()
                .HasPrecision(18, 2);              
                   
            builder.Property<Guid>("SaleId")
                .IsRequired();
        }
    }
}
