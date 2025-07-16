using Kervan.SharedKernel.Abstractions;
using Kervan.SharedKernel.Application.Messaging;
using Kervan.SharedKernel.Infrastructure.Services.Messaging;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.OpenApi.Services;
using System.Reflection;

namespace Kervan.API.Extensions;

public static class ModuleExtensions
{
    // Bu metot, IServiceCollection'ı genişletir.
    // Tüm modülleri bulur, servislerini kaydeder ve modül listesini de ileride kullanmak üzere DI'a ekler.
    public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. ADIM: Modül DLL'lerini Disk'ten Manuel Olarak Bul ve Yükle
        // Uygulamanın çalıştığı ana dizini alıyoruz.
        var location = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        if (string.IsNullOrWhiteSpace(location))
        {
            // Bu durum genellikle test senaryolarında veya özel host'larda olur.
            // Güvenli bir fallback sağlıyoruz.
            location = AppContext.BaseDirectory;
        }

        // Modül isimlendirme standardımıza uyan tüm DLL dosyalarını buluyoruz.
        var moduleAssemblies = Directory.GetFiles(location, "Kervan.Modules.*.dll")
                                      .Select(Assembly.LoadFrom)
                                      .ToList();

        // 2. ADIM: IModule'ü implemente eden tipleri bul
        // Artık sadece bizim yüklediğimiz bu assembly'leri tarıyoruz.
        var modules = moduleAssemblies
            .SelectMany(a => a.GetTypes())
            .Where(p => p.IsClass && !p.IsAbstract && typeof(IModule).IsAssignableFrom(p))
            .Select(Activator.CreateInstance)
            .Cast<IModule>()
            .ToList();

        // 2. Her modülün kendi servislerini kaydetmesini sağla.
        foreach (var module in modules)
        {
            module.Register(services, configuration);
        }

        // 3. Bulunan modül listesini ve API assembly'lerini daha sonra kullanmak üzere kaydet.
        services.AddSingleton(modules as IReadOnlyCollection<IModule>);

        services.AddScoped<IMediator, Mediator>();

        // Scrutor ile tüm Handler tiplerini otomatik olarak bulup kaydediyoruz.
        services.Scan(selector => selector
            .FromAssemblies(moduleAssemblies)
            .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );

        // CACHE ISITMA (EAGER LOADING)
        // Tüm handler tiplerini bul
        var handlerTypes = moduleAssemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
            .ToList();


        // Her bir handler'ın request tipini bul ve fabrika'yı tetikleyerek cache'i doldur.
        foreach (var handlerType in handlerTypes)
        {
            var requestType = handlerType.GetInterfaces()
                .First(i => i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))
                .GetGenericArguments()[0];

            RequestHandlerFactory.GetRunner(requestType);
        }


        var apiAssemblies = modules.Select(m => m.GetType().Assembly).Distinct().ToList();
        services.AddControllers()
            .ConfigureApplicationPartManager(manager =>
            {
                foreach (var assembly in apiAssemblies)
                {
                    // AssemblyPart, bu assembly içindeki Controller'ların bulunmasını sağlar.
                    manager.ApplicationParts.Add(new AssemblyPart(assembly));
                }
            });

        return services;
    }

    // Bu metot, IApplicationBuilder'ı genişletir.
    // Başlangıçta kaydedilen modülleri kullanarak her birinin middleware'ini ekler.
    public static IApplicationBuilder UseModules(this IApplicationBuilder app)
    {
        // DI konteynerinden, başlangıçta bulduğumuz modül listesini alıyoruz.
        var modules = app.ApplicationServices.GetRequiredService<IReadOnlyCollection<IModule>>();

        foreach (var module in modules)
        {
            module.Use(app);
        }

        return app;
    }
}