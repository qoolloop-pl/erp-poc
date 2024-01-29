using Ardalis.Specification;

namespace ErpModule.Shared;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}
