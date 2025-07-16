using Kervan.Core.Application.Interfaces;
using Kervan.Infrastructure.Services.Caching;
using Microsoft.Extensions.DependencyInjection;

namespace Kervan.Core.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraServices(this IServiceCollection services)
    {
        services.AddMemoryCache(); // .NET'in dahili memory cache servisini ekler.
        services.AddSingleton<ICacheService, InMemoryCacheService>(); // Bizim soyutlamamızı kaydeder.

        return services;
    }
}