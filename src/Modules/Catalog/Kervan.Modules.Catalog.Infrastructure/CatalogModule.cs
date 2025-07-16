using Kervan.SharedKernel.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kervan.Modules.Catalog.Infrastructure;

public class CatalogModule : IModule
{
    public string Name => "Catalog";

    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddCatalogInfrastructure(configuration);

    }

    public void Use(IApplicationBuilder app)
    {
        
    }
}