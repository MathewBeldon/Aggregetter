using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticlePagedList
{
    public sealed class GetArticlePagedListQueryValidation : AbstractValidator<GetArticlePagedListQuery>
    {
        public GetArticlePagedListQueryValidation()
        {
            RuleFor(pr => pr.pageSize)
                .InclusiveBetween(1, 20)
                .WithMessage("<pageSize> should be within 1 and 20");

            RuleFor(pr => pr.page)
                .GreaterThan(0)
                .WithMessage("<page> should be greater than 0");
        }
    }
}
