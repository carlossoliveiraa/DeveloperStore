using DeveloperStore.Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeveloperStore.Sales.Infrastructure.Persistence.Mappings
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

            builder.Property(item => item.Total)
                .HasPrecision(18, 2)
                .HasComputedColumnSql("([UnitPrice] * [Quantity]) - [Discount]", stored: true); // Opcional, se usar SQL Server

            //Do not try to map this property, it only exists in the domain
            builder.Ignore(x => x.Total);
                   
            builder.Property<Guid>("SaleId")
                .IsRequired();
        }
    }
}
