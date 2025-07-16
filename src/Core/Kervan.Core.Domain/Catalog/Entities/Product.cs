using Kervan.Core.Domain.Catalog.ValueObjects;
using Kervan.Core.Domain.Shared; // Entity base class'ımızı kullanmak için ekliyoruz.

namespace Kervan.Core.Domain.Catalog.Entities;

public class Product : Entity<Guid>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    // Henüz Value Object oluşturmadığımız için Fiyat'ı şimdilik decimal tutalım.
    public Money Price { get; private set; }
    public int StockQuantity { get; private set; }

    // Bu, veritabanından nesne oluşturulurken EF Core'un kullanacağı private constructor.
    private Product() : base(Guid.NewGuid()) { }

    // Bu, uygulama kodunda yeni bir ürün yaratmak için kullanacağımız public constructor.
    public Product(string name, string description, Money price, int stockQuantity) : base(Guid.NewGuid())
    {
        // Burada kurallarımızı (validasyon) uygulayabiliriz.
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be empty.", nameof(name));

        if (price.Amount < 0)
            throw new ArgumentException("Price cannot be negative.", nameof(price));

        Name = name;
        Description = description;
        Price = price;
        StockQuantity = stockQuantity;
    }

    // Davranış Metotları (Behaviors)
    public void ChangePrice(Money newPrice)
    {
        if (newPrice.Amount < 0)
            throw new ArgumentException("Price cannot be negative.", nameof(newPrice));

        Price = newPrice;
        // İleride burada fiyat değişikliği ile ilgili bir Domain Event fırlatabiliriz.
    }

    public void DecreaseStock(int quantity)
    {
        if (StockQuantity < quantity)
            throw new InvalidOperationException("Not enough stock.");

        StockQuantity -= quantity;
    }
}