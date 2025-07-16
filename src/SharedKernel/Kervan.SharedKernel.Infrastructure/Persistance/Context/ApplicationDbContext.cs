using Kervan.SharedKernel.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kervan.Infrastructure.Persistence.Contexts;

public class ApplicationDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ÖNEMLİ: base.OnModelCreating'i en başta çağırmalıyız.
        // Bu, Identity'nin kendi tablolarını ve ilişkilerini doğru şekilde kurmasını sağlar.
        base.OnModelCreating(modelBuilder);

        // Bu sihirli satır, bu projenin (Persistence) assembly'sini tarar,
        // IEntityTypeConfiguration<T> arayüzünü implemente eden tüm sınıfları bulur
        // ve içindeki konfigürasyonları otomatik olarak uygular.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}