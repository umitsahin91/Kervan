using Kervan.SharedKernel.Domain.Shared;
using Kervan.SharedKernel.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kervan.SharedKernel.Domain.Repositories;

public interface IRepository<T> where T : Entity<T>, new()
{
    Task<T?> GetFirstOrDefaultAsync(IRepositorySpecification<T> spec, CancellationToken cancellationToken = default);
    Task<List<T>> GetAllAsync(IRepositorySpecification<T> spec, CancellationToken cancellationToken = default);
    Task<int> CountAsync(IRepositorySpecification<T> spec, CancellationToken cancellationToken = default);

    void Add(T entity);
    void Update(T entity);
    void Remove(T entity);
}
