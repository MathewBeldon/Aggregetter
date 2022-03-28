using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Services.PaginationService;
using Aggregetter.Aggre.Application.Settings;
using Aggregetter.Aggre.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByProviderAndCategory
{
    public sealed class GetArticlesByProviderAndCategoryQueryValidator : PaginationValidatorBase<GetArticlesByProviderAndCategoryQuery>
    {
        private readonly IBaseRepository<Provider> _baseProviderRepository;
        private readonly IBaseRepository<Category> _baseCategoryRepository;

        public GetArticlesByProviderAndCategoryQueryValidator(IBaseRepository<Provider> baseProviderRepository, IBaseRepository<Category> baseCategoryRepository,
            IOptions<PagedSettings> settings) : base(settings)
        {
            _baseProviderRepository = baseProviderRepository ?? throw new ArgumentNullException(nameof(baseProviderRepository));
            _baseCategoryRepository = baseCategoryRepository ?? throw new ArgumentNullException(nameof(baseCategoryRepository));

            RuleFor(query => query.ProviderId)
                .MustAsync(async (providerId, cancellationToken) => {
                    return await ProviderExistsAsync(providerId, cancellationToken);
                }).WithMessage("Enter a valid provider");

            RuleFor(query => query.CategoryId)
                .MustAsync(async (categoryId, cancellationToken) => {
                    return await CategoryExistsAsync(categoryId, cancellationToken);
                }).WithMessage("Enter a valid category");
        }

        private async Task<bool> ProviderExistsAsync(int providerId, CancellationToken cancellationToken)
        {
            return await _baseProviderRepository.CheckExistsByIdAsync(providerId, cancellationToken);
        }

        private async Task<bool> CategoryExistsAsync(int categoryId, CancellationToken cancellationToken)
        {
            return await _baseCategoryRepository.CheckExistsByIdAsync(categoryId, cancellationToken);
        }
    }
}
