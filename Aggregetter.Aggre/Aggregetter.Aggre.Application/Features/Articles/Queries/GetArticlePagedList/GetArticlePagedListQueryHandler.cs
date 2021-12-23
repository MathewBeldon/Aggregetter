using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Domain.Entities;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticlePagedList
{
    public class GetArticlePagedListQueryHandler : IRequestHandler<GetArticlePagedListQuery, GetArticlePagedListQueryResponse>
    { 
        private readonly IBaseRepository<Article> _articleRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<GetArticlePagedListQuery> _validator;
        private readonly IConfiguration _configuration;

        public GetArticlePagedListQueryHandler(IBaseRepository<Article> articleRepository, 
            IMapper mapper,
            IValidator<GetArticlePagedListQuery> validator,
            IConfiguration configuration)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
            _validator = validator;
            _configuration = configuration;
        }
        public async Task<GetArticlePagedListQueryResponse> Handle(GetArticlePagedListQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (validationResult.IsValid)
            {

                var response = await _articleRepository.GetPagedResponseAsync(request.page, request.pageSize, cancellationToken);
                var getArticlePagedItemList = _mapper.Map<List<GetArticlePagedItemDto>>(response);

                return new GetArticlePagedListQueryResponse(request.page, request.pageSize, getArticlePagedItemList);
            }

            int defaultPage = 1;
            int defaultPageSize = int.Parse(_configuration["Defaults:PageSize"]);

            return new GetArticlePagedListQueryResponse(defaultPage, defaultPageSize, null)
            {
                ValidationErrors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
            };
        }
    }
}
