using Ardalis.Specification;

namespace ErpModule.Shared;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}