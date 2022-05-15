using Aggregetter.Aggre.Application.Contracts.Mediator.Transactions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Persistence.Pipelines
{
    public sealed class TransactionPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IWriteRequest<TResponse>
    {
        private readonly AggreDbContext _context;
        public TransactionPipelineBehaviour(AggreDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse result;

            //transactions not supported with in memory, fix by using docker with tests
            if (_context.Database.IsInMemory())
            {
                return await next();
            }

            var strategy = _context.Database.CreateExecutionStrategy();
            result = await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                result = await next();
                await transaction.CommitAsync();
                return result;          
            });

            return result;
        }
    }
}
