// Kervan.Core.Application/Features/Products/Queries/GetProductById/GetProductByIdQueryHandler.cs
using Kervan.Core.Application.Features.Products.Contracts;
using Kervan.Core.Domain.Catalog.Interfaces; // IProductRepository için
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kervan.Core.Application.Features.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponse?>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductResponse?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        // 1. Repository'yi kullanarak veritabanından Entity'yi çekiyoruz.
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

        // 2. Ürün bulunamadıysa null dönüyoruz. Controller bunu 404 Not Found'a çevirecek.
        if (product is null)
        {
            return null;
        }

        // 3. Entity'yi, dış dünyaya sunacağımız DTO'ya dönüştürüyoruz (Mapping).
        return new ProductResponse(
            product.Id,
            product.Name,
            product.Description,
            product.Price.Amount,
            product.Price.Currency,
            product.StockQuantity);
    }
}