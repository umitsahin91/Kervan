using Kervan.Core.Application.Features.Products.Commands.CreateProduct;
using Kervan.Core.Application.Features.Products.Queries.GetProductById;
using MediatR; // ISender için
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kervan.Presentation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    // MediatR'ı doğrudan kullanmıyoruz, onun gönderim arayüzü olan ISender'ı enjekte ediyoruz.
    private readonly ISender _sender;

    public ProductsController(ISender sender)
    {
        _sender = sender;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
    {
        var productId = await _sender.Send(command);

        // HTTP 201 Created durum kodu ile birlikte yeni ürünün Id'sini döndürüyoruz.
        return CreatedAtAction(nameof(GetProductById), new { id = productId }, productId);
    }

    [HttpGet("{id:guid}")] // Route parametresinin bir GUID olması gerektiğini belirtiyoruz.
    public async Task<IActionResult> GetProductById(Guid id)
    {
        // 1. Gelen ID ile bir Query nesnesi oluşturuyoruz.
        var query = new GetProductByIdQuery(id);

        // 2. Query'yi MediatR'a gönderip cevabı (ProductResponse veya null) alıyoruz.
        var product = await _sender.Send(query);

        // 3. Dönen sonuca göre HTTP cevabı oluşturuyoruz.
        return product is not null ? Ok(product) : NotFound();
    }
}