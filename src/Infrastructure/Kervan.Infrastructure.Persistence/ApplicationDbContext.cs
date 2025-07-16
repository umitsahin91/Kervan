// --- ApplicationDbContext.cs DOSYASINDAKİ DEĞİŞİKLİKLER ---

using Kervan.Core.Application.Interfaces;
using Kervan.Core.Domain.Catalog.Entities;
using Kervan.Core.Domain.Users.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // IdentityDbContext için
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Kervan.Infrastructure.Persistence;

// DbContext yerine IdentityDbContext'ten miras alıyoruz ve kendi AppUser'ımızı belirtiyoruz.
public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ÖNEMLİ: base.OnModelCreating(modelBuilder) çağrısı en başta olmalı.
        // Bu, Identity'nin kendi tablolarını ve ilişkilerini doğru şekilde kurmasını sağlar.
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}