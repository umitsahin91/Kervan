// Kervan.Core.Application/Features/Products/Queries/GetProductById/GetProductByIdQuery.cs
using Kervan.Core.Application.Features.Products.Contracts; // ProductResponse için
using MediatR;
using System;

namespace Kervan.Core.Application.Features.Products.Queries.GetProductById;

// Bu sorgu işlendiğinde, bir ProductResponse nesnesi döneceğini belirtiyoruz.
// Dönecek olan nesne null olabileceği için `ProductResponse?` kullanıyoruz.
public record GetProductByIdQuery(Guid Id) : IRequest<ProductResponse?>;