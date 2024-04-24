using MediatR;

namespace Kavenegar.Application.BuildingBlocks.CQRS;

public interface ICommand : ICommand<Unit>
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
