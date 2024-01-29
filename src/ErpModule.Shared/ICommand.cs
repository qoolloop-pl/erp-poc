using MediatR;

namespace ErpModule.Shared;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
