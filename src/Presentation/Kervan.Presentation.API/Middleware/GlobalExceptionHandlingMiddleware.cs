// Kervan.Presentation.API/Middleware/GlobalExceptionHandlingMiddleware.cs
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace Kervan.Presentation.API.Middleware;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            // Hata yoksa, bir sonraki middleware'e devam et.
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Beklenmedik bir hata oluştu: {Message}", e.Message);

            // Hata varsa, yakala ve özel bir cevap oluştur.
            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        // Değişkenlerimizi önceden tanımlıyoruz.
        int statusCode;
        object response;

        // Klasik switch-case bloğunu kullanıyoruz.
        switch (exception)
        {
            // Gelen hata bir ValidationException ise...
            case ValidationException validationException:
                statusCode = StatusCodes.Status400BadRequest;
                response = new
                {
                    Title = "Validation Error",
                    Status = StatusCodes.Status400BadRequest,
                    Errors = validationException.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
                };
                break; // Case'i sonlandır.

            // Diğer tüm hatalar için...
            default:
                statusCode = StatusCodes.Status500InternalServerError;
                response = new
                {
                    Title = "Server Error",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = "Sunucuda beklenmedik bir hata oluştu. Lütfen daha sonra tekrar deneyin."
                };
                break; // Case'i sonlandır.
        }


        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}