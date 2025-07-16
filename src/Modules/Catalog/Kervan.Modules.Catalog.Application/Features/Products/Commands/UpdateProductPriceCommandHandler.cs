using Kervan.Modules.Catalog.Application.Features.Products.Commands.UpdateProductPrice;
using Kervan.SharedKernel.Application.Messaging;

namespace Kervan.Modules.Catalog.Application.Features.Products.Commands;
public class UpdateProductPriceCommandHandler : IRequestHandler<UpdateProductPriceCommand, bool>
{
    public async Task<bool> Handle(UpdateProductPriceCommand request, CancellationToken cancellationToken)
    {
        return true;
    }
}