using Aggregetter.Aggre.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Articles.Commands.CreateArticle
{
    public sealed class CreateArticleCommandResponse : BaseResponse
    {
        public CreateArticleCommandResponse() : base() { }

        public CreateArticleDto Article { get; set; }
    }
}
