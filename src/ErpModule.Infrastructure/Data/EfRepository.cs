using Ardalis.Result;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using ErpModule.Shared;
using ErpModule.Shared.Specification;
using ErpModule.Shared.Specification.List;
using Microsoft.EntityFrameworkCore;

namespace ErpModule.Infrastructure.Data;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(ErpDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<PagedList<T>> ListPagedAsync(ISpecification<T> specification, ListFilterBase filter, CancellationToken cancellationToken = default)
    {
        var count = await ApplySpecification(specification).CountAsync(cancellationToken);

        var paging = new Pagination(filter, count);

        var items = await ApplySpecification(specification)
            .Skip(paging.Skip)
            .Take(paging.Take)
            .ToListAsync(cancellationToken);

        return new PagedList<T>(items, paging);
    }
}
