using Kervan.Core.Domain.Catalog.Entities;
using Kervan.Core.Domain.Catalog.Interfaces;
using Microsoft.EntityFrameworkCore; // .ToListAsync() ve diğer EF metotları için
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kervan.Infrastructure.Persistence.Repositories;

// IProductRepository sözleşmesini uyguluyoruz.
public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    // Dependency Injection ile ApplicationDbContext'i alıyoruz.
    // Bu sınıf, veritabanıyla nasıl konuşacağını bilmek için DbContext'e ihtiyaç duyar.
    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(Product product)
    {
        // EF Core'un "Change Tracker" mekanizması sayesinde,
        // DbContext'e bu ürünün "eklenmek üzere" olduğunu söylüyoruz.
        // Henüz veritabanına bir sorgu GİTMİYOR.
        _context.Products.Add(product);
    }

    public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        // _context.Products, veritabanındaki Products tablosunu temsil eder.
        // ToListAsync() metodu, EF Core'un tüm satırları çekip Product nesnelerine
        // dönüştürmesini sağlar. Bu, gerçek bir veritabanı sorgusu çalıştırır.
        return await _context.Products.ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        // FindAsync metodu, primary key üzerinden arama yapmak için en verimli yoldur.
        // Önce bellekte (change tracker) arar, bulamazsa veritabanına gider.
        return await _context.Products.FindAsync(new object[] { id }, cancellationToken);
    }
}