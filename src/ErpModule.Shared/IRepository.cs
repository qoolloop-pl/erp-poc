using Ardalis.Specification;
using ErpModule.Shared.Specification;
using ErpModule.Shared.Specification.List;

namespace ErpModule.Shared;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
    public Task<PagedList<T>> ListPagedAsync(ISpecification<T> specification, ListFilterBase filter,
        CancellationToken cancellationToken = default);
}
