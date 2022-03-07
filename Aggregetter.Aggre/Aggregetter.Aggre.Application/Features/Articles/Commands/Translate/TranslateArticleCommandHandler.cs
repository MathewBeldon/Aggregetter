using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Articles.Commands.Translate
{
    public sealed class TranslateArticleCommandHandler : IRequestHandler<TranslateArticleCommand, TranslateArticleCommandResponse>
    {
        public Task<TranslateArticleCommandResponse> Handle(TranslateArticleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
