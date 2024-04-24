using MediatR;

namespace Kavenegar.Application.BuildingBlocks.CQRS;
public interface IQuery<out TResponse> : IRequest<TResponse>  
    where TResponse : notnull
{
}
