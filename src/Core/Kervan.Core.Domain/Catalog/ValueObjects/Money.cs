// Kervan.Core.Domain/Catalog/ValueObjects/Money.cs
using Kervan.Core.Domain.Shared;

namespace Kervan.Core.Domain.Catalog.ValueObjects;

// Artık ValueObject'ten miras alıyor.
public class Money : ValueObject
{
    public decimal Amount { get; } // set'i tamamen kaldırdık!
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        if (amount < 0)
            throw new ArgumentException("Money amount cannot be negative.", nameof(amount));

        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency cannot be empty.", nameof(currency));

        Amount = amount;
        Currency = currency.ToUpper(); // Para birimini standartlaştıralım.
    }

    // Base class'a eşitlik için hangi bileşenleri kullanacağını söylüyoruz.
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}