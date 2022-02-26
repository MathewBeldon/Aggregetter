using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Categories.Queries.GetCategories
{
    public sealed class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, GetProvidersQueryREsponse>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Category> _categoryRepository;

        public GetCategoriesQueryHandler(IMapper mapper, 
            IBaseRepository<Category> categoryRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<GetProvidersQueryREsponse> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categoryEntities = await _categoryRepository.GetAllAsync(cancellationToken);

            var categoriesDtos = _mapper.Map<List<GetCategoriesDto>>(categoryEntities);

            return new GetProvidersQueryREsponse(categoriesDtos);
        }
    }
}
