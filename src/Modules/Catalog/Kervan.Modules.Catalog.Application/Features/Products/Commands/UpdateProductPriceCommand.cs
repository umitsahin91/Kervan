using Kervan.SharedKernel.Application.Messaging;
using System;

namespace Kervan.Modules.Catalog.Application.Features.Products.Commands.UpdateProductPrice;


public record UpdateProductPriceCommand(Guid ProductId, decimal NewPriceAmount, string NewPriceCurrency) : IRequest<bool>;