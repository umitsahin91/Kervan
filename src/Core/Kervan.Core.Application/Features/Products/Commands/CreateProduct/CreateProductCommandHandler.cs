using FluentValidation;
using Kervan.Core.Application.Interfaces;
using Kervan.Core.Domain.Catalog.Entities;
using Kervan.Core.Domain.Catalog.Interfaces;
using Kervan.Core.Domain.Catalog.ValueObjects;
using MediatR;

namespace Kervan.Core.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateProductCommand> _validator;

    // Bağımlılıklarımızı constructor üzerinden enjekte ediyoruz.
    public CreateProductCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        IValidator<CreateProductCommand> validator)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        // 2. ADIM: Doğrulama başarılıysa, asıl iş mantığına devam et.
        var price = new Money(request.PriceAmount, request.PriceCurrency);
        var product = new Product(
            request.Name,
            request.Description,
            price,
            request.StockQuantity);

        _productRepository.Add(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}