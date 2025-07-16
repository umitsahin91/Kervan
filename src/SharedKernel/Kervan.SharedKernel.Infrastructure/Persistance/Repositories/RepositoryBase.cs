using Kervan.SharedKernel.Domain.Specifications;

namespace Kervan.SharedKernel.Infrastructure.Persistance.Repositories;

public class RepositoryBase<T> where T : class
{
    //private readonly DbContext _context; // DİKKAT: Modüle özel DbContext
    //private readonly DbSet<T> _dbSet;

    //public RepositoryBase(DbContext context)
    //{
    //    _context = context;
    //    _dbSet = _context.Set<T>();
    //}

    //// Specification'ı EF Core sorgusuna çeviren merkezi metot
    //private IQueryable<T> ApplySpecification(IRepositorySpecification<T> spec)
    //{
    //    IQueryable<T> query = _dbSet;

    //    if (spec.Criteria != null)
    //        query = query.Where(spec.Criteria);

    //    query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

    //    if (spec.OrderBy != null)
    //        query = query.OrderBy(spec.OrderBy);
    //    else if (spec.OrderByDescending != null)
    //        query = query.OrderByDescending(spec.OrderByDescending);

    //    if (spec.IsPagingEnabled)
    //        query = query.Skip(spec.Skip).Take(spec.Take);

    //    return query;
    //}

    //// Ortak metotların implementasyonları
    //public void Add(T entity) => _dbSet.Add(entity);
    //public void Update(T entity) => _dbSet.Update(entity);
    //public void Remove(T entity) => _dbSet.Remove(entity);
    //public async Task<int> CountAsync(IRepositorySpecification<T> spec, CancellationToken cancellationToken) => await ApplySpecification(spec).CountAsync(cancellationToken);
    //public async Task<List<T>> GetAllAsync(IRepositorySpecification<T> spec, CancellationToken cancellationToken) => await ApplySpecification(spec).ToListAsync(cancellationToken);
    //public async Task<T?> GetFirstOrDefaultAsync(IRepositorySpecification<T> spec, CancellationToken cancellationToken) => await ApplySpecification(spec).FirstOrDefaultAsync(cancellationToken);
}