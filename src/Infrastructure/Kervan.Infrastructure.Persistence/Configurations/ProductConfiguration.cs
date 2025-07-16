using Kervan.Core.Domain.Catalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kervan.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        // Tablo adını belirliyoruz.
        builder.ToTable("Products");

        // Primary Key olarak Id alanını belirliyoruz.
        builder.HasKey(p => p.Id);

        // Name özelliğinin zorunlu (required) ve maksimum 200 karakter olacağını belirtiyoruz.
        builder.Property(p => p.Name).IsRequired().HasMaxLength(200);

        // Money Value Object'inin veritabanına nasıl eşleneceğini tanımlıyoruz.
        // EF Core 8+ ile bu çok daha kolay. "Complex Types" olarak geçer.
        // Daha eski sürümlerde "Owned Types" kullanılır. Biz modern yolu izliyoruz.
        builder.ComplexProperty(p => p.Price, priceBuilder =>
        {
            priceBuilder.Property(m => m.Amount).HasColumnName("Price_Amount");
            priceBuilder.Property(m => m.Currency).HasColumnName("Price_Currency").HasMaxLength(3);
        });
    }
}