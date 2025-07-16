using MediatR; // IRequest için

namespace Kervan.Core.Application.Features.Products.Commands.CreateProduct;

// C# 9 ve sonrası için 'record' tipi, değişmez veri paketleri için mükemmeldir.
// Bu komut işlendiğinde, yeni oluşturulan ürünün Guid'sini döndüreceğini belirtiyoruz (IRequest<Guid>).
public record CreateProductCommand(
    string Name,
    string Description,
    decimal PriceAmount,
    string PriceCurrency,
    int StockQuantity) : IRequest<Guid>;