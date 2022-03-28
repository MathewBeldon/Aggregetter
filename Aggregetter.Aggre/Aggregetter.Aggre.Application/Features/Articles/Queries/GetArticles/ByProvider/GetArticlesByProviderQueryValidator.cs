using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Services.PaginationService;
using Aggregetter.Aggre.Application.Settings;
using Aggregetter.Aggre.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByProvider
{
    public sealed class GetArticlesByProviderQueryValidator : PaginationValidatorBase<GetArticlesByProviderQuery>
    {
        private readonly IBaseRepository<Provider> _baseProviderRepository;
        public GetArticlesByProviderQueryValidator(IBaseRepository<Provider> baseProviderRepository, IOptions<PagedSettings> settings) : base(settings)
        {
            _baseProviderRepository = baseProviderRepository ?? throw new ArgumentNullException(nameof(baseProviderRepository));

            RuleFor(query => query.ProviderId)
                .MustAsync(async (providerId, cancellationToken) => {
                    return await ProviderExistsAsync(providerId, cancellationToken);
                }).WithMessage("Enter a valid provider");
        }

        private async Task<bool> ProviderExistsAsync(int providerId, CancellationToken cancellationToken)
        {
            return await _baseProviderRepository.CheckExistsByIdAsync(providerId, cancellationToken);
        }
    }
}
