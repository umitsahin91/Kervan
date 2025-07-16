using Kervan.API.Extensions;
using Kervan.SharedKernel.Infrastructure;
using Microsoft.OpenApi.Models; // << BU using'i ekliyoruz
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();
Log.Information("Kervan Host API Başlatılıyor...");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration));

    // Katmanların ve Modüllerin servislerini ekliyoruz.
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddModules(builder.Configuration);

    builder.Services.AddEndpointsApiExplorer();

    // <<<<<<<<<<<<<<< YENİ SWAGGER YAPILANDIRMASI BAŞLANGICI >>>>>>>>>>>>>>>
    builder.Services.AddSwaggerGen(options =>
    {
        // Swagger dokümantasyonunun başlığını ve versiyonunu ayarla
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Kervan API",
            Description = "Kervan E-Ticaret Platformu için Modüler Monolit API",
            Contact = new OpenApiContact
            {
                Name = "Kervan Projesi",
                Url = new Uri("https://github.com/") // Buraya projenin GitHub adresi gelebilir
            }
        });

        // JWT Bearer token'ı için Swagger'a bir güvenlik tanımı ekle
        // Bu, arayüzde "Authorize" butonunu oluşturur.
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Lütfen 'Bearer' yazdıktan sonra bir boşluk bırakıp JWT token'ınızı girin.",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        // Swagger UI'da "Authorize" butonunun çalışması için güvenlik gereksinimini ekle
        // Bu, her endpoint'in yanında bir kilit ikonu gösterir ve token'ın isteklere eklenmesini sağlar.
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
        });
    });
    // <<<<<<<<<<<<<<< YENİ SWAGGER YAPILANDIRMASI SONU >>>>>>>>>>>>>>>

    // TODO: Auth servisleri buraya gelecek

    var app = builder.Build();

    // Pipeline (Middleware) Yapılandırması
    app.UseSerilogRequestLogging();

    // Bu tek satır, tüm modüllerin kendi Use(...) metotlarını çağırır.
    app.UseModules();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            // Swagger UI'ın hangi endpoint'i kullanacağını belirtiyoruz.
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kervan API v1");
            // Sayfayı açtığımızda endpoint listesinin kapalı gelmesini sağlar.
            c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        });
    }

    // TODO: Error Handling, Auth middleware'leri buraya gelecek
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host API başlatılırken kritik bir hata oluştu.");
}
finally
{
    Log.Information("Kervan Host API Kapatılıyor...");
    Log.CloseAndFlush();
}