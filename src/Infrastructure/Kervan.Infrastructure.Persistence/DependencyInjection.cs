// Kervan.Infrastructure.Persistence/DependencyInjection.cs
using Kervan.Core.Application.Interfaces; // IUnitOfWork için
using Kervan.Core.Domain.Catalog.Interfaces; // IProductRepository için
using Kervan.Infrastructure.Persistence.Repositories; // ProductRepository için
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration; // IConfiguration için
using Microsoft.Extensions.DependencyInjection; // IServiceCollection için

namespace Kervan.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // 1. DbContext'i Kaydetmek
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        // 2. Repository'leri Kaydetmek
        // Ne zaman IProductRepository istenirse, ona ProductRepository ver.
        services.AddScoped<IProductRepository, ProductRepository>();

        // 3. Unit of Work'ü Kaydetmek
        // IUnitOfWork istendiğinde, ona ApplicationDbContext'in kendisini ver.
        // Çünkü ApplicationDbContext bu interface'i zaten uyguluyor.
        services.AddScoped<IUnitOfWork>(sp =>
            sp.GetRequiredService<ApplicationDbContext>());

        return services;
    }
}