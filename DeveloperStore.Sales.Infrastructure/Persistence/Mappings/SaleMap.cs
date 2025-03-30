using DeveloperStore.Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeveloperStore.Sales.Infrastructure.Persistence.Mappings
{
    public sealed class SaleMap : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.SaleNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.SaleDate)
                .IsRequired();

            builder.Property(s => s.CustomerId)
                .IsRequired();

            builder.Property(s => s.CustomerName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.BranchId)
                .IsRequired();

            builder.Property(s => s.BranchName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.IsCancelled)
                .IsRequired();

            // Relacionamento: Sale -> SaleItems (1:N)
            builder.HasMany(s => s.Items)
                   .WithOne()
                   .HasForeignKey("SaleId") // Shadow property
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
