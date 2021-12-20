using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails
{
    public class GetArticleDetailsQueryHandler : IRequestHandler<GetArticleDetailsQuery, ArticleDetailsVm>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IBaseRepository<Provider> _providerRepository;
        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly IBaseRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public GetArticleDetailsQueryHandler(IArticleRepository articleRepository, 
                                             IBaseRepository<Provider> providerRepository,
                                             IBaseRepository<Category> categoryRepository,
                                             IBaseRepository<Language> languageRepository,
                                             IMapper mapper)
        {
            _articleRepository = articleRepository;
            _providerRepository = providerRepository;
            _categoryRepository = categoryRepository;
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<ArticleDetailsVm> Handle(GetArticleDetailsQuery request, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetArticleBySlugAsync(request.ArticleSlug, cancellationToken);
            var provider = await _providerRepository.GetByIdAsync(article.ProviderId, cancellationToken);
            var category = await _categoryRepository.GetByIdAsync(article.CategoryId, cancellationToken);
            var language = await _languageRepository.GetByIdAsync(provider.LanguageId, cancellationToken);

            var articleDetailsVm = _mapper.Map<ArticleDetailsVm>(article);
            articleDetailsVm.Provider = _mapper.Map<ProviderDto>(provider);
            articleDetailsVm.Category = _mapper.Map<CategoryDto>(category);
            articleDetailsVm.Provider.Language = _mapper.Map<LanguageDto>(language);

            return articleDetailsVm;
        }
    }
}
