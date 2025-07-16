using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Kervan.SharedKernel.Application.Messaging;
using Kervan.Modules.Catalog.Application.Features.Products.Commands.UpdateProductPrice;
using Kervan.SharedKernel.Application.Common.Wrappers;

namespace Kervan.Modules.Catalog.API.Endpoints.Products;

// İstek gövdesi için bir DTO
public record UpdateProductPriceRequest(decimal PriceAmount, string PriceCurrency);

[ApiController]
[Route("api/products")]
public class UpdateProductPriceEndpoint : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateProductPriceEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet] // Route: PUT /api/products/123-abc/price
    public async Task<IActionResult> UpdatePrice()
    {
        var command = new UpdateProductPriceCommand(
            Guid.NewGuid(),
            0,
            "request.PriceCurrency");

        var result = await _mediator.Send(command);

        if (!result)
        {
            return NotFound(ApiResponse<object>.Fail($"Product with Id not found.", 404));
        }

        return Ok(ApiResponse<object>.Success(200));
    }
}