using Kervan.Core.Domain.Catalog.Entities; // Product sınıfını kullanabilmek için.

namespace Kervan.Core.Domain.Catalog.Interfaces;

public interface IProductRepository
{
    /// <summary>
    /// Verilen kimliğe sahip ürünü asenkron olarak bulur.
    /// </summary>
    /// <param name="id">Aranacak ürünün kimliği.</param>
    /// <param name="cancellationToken">Operasyonu iptal etmek için kullanılabilecek token.</param>
    /// <returns>Bulunan ürün nesnesi veya bulunamazsa null.</returns>
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Tüm ürünleri asenkron olarak listeler.
    /// </summary>
    /// <returns>Tüm ürünlerin bir listesi.</returns>
    Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Yeni bir ürün ekler.
    /// Not: Bu metot, ürünü "eklenmek üzere" işaretler. Gerçek kaydetme işlemi Unit of Work tarafından yapılır.
    /// </summary>
    /// <param name="product">Eklenecek ürün nesnesi.</param>
    void Add(Product product);
}