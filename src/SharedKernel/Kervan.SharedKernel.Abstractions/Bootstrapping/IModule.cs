using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kervan.SharedKernel.Abstractions;

// Bu, Host ile Modüller arasındaki tek sözleşmedir.
public interface IModule
{
    // Modülün adını belirtmek, loglama ve tanılama için faydalıdır.
    string Name { get; }

    // Modülün kendi servislerini DI konteynerine nasıl kaydedeceğini tanımlar.
    void Register(IServiceCollection services, IConfiguration configuration);

    // Modülün HTTP pipeline'ına kendi middleware'lerini nasıl ekleyeceğini tanımlar.
    void Use(IApplicationBuilder app);
}