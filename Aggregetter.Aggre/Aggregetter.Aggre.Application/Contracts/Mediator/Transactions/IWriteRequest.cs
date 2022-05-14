using MediatR;

namespace Aggregetter.Aggre.Application.Contracts.Mediator.Transactions
{
    public interface IWriteRequest<TResponse> : IRequest<TResponse>
    {
    }
}
