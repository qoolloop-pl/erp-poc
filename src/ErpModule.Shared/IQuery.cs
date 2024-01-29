using MediatR;

namespace ErpModule.Shared;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}