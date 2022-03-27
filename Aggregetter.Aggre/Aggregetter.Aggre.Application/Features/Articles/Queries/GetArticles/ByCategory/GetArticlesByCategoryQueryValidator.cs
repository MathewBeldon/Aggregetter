using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Services.PaginationService;
using Aggregetter.Aggre.Application.Settings;
using Aggregetter.Aggre.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByCategory
{
    public sealed class GetArticlesByCategoryQueryValidator : PaginationValidatorBase<GetArticlesByCategoryQuery>
    {
        private readonly IBaseRepository<Category> _baseCategoryRepository;
        public GetArticlesByCategoryQueryValidator(IBaseRepository<Category> baseCategoryRepository, IOptions<PagedSettings> settings) : base(settings)
        {
            _baseCategoryRepository = baseCategoryRepository ?? throw new ArgumentNullException(nameof(baseCategoryRepository));

            RuleFor(query => query.CategoryId)
                .MustAsync(async (categoryId, cancellationToken) => {
                    return await CategoryExistsAsync(categoryId, cancellationToken);
                }).WithMessage("Enter a valid category");
        }

        private async Task<bool> CategoryExistsAsync(int categoryId, CancellationToken cancellationToken)
        {            
             return await _baseCategoryRepository.CheckExistsByIdAsync(categoryId, cancellationToken);               
        }
    }
}
