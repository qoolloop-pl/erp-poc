using Ardalis.Specification.EntityFrameworkCore;
using ErpModule.Shared;

namespace ErpModule.Infrastructure.Data;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(ErpDbContext dbContext) : base(dbContext)
    {
    }
}
