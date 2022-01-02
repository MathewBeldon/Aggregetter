using FluentValidation;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticlePagedList
{
    public sealed class GetArticlePagedListQueryValidator : AbstractValidator<GetArticlePagedListQuery>
    {
        public GetArticlePagedListQueryValidator()
        {
            RuleFor(pr => pr.PagedRequest.PageSize)
                .InclusiveBetween(1, 20)
                .WithMessage("<pageSize> should be within 1 and 20");

            RuleFor(pr => pr.PagedRequest.Page)
                .GreaterThan(0)
                .WithMessage("<page> should be greater than 0");
        }
    }
}
