namespace Kervan.Core.Application.Features.Products.Contracts;

// Bu, API'dan dışarıya döneceğimiz ürün modelidir.
public record ProductResponse(
    Guid Id,
    string Name,
    string Description,
    decimal PriceAmount,
    string PriceCurrency,
    int StockQuantity);