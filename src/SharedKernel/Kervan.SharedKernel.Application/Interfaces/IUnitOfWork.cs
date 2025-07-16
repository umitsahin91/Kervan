namespace Kervan.SharedKernel.Application.Interfaces;

public interface IUnitOfWork
{
    /// <summary>
    /// Bellekte takip edilen tüm değişiklikleri tek bir işlem olarak veritabanına kaydeder.
    /// </summary>
    /// <param name="cancellationToken">Operasyonu iptal etmek için kullanılabilecek token.</param>
    /// <returns>Veritabanında etkilenen satır sayısı.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}