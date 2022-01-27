﻿using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Categories.Queries.GetCategories
{
    public sealed class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, GetCategoriesQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Category> _categoryRepository;

        public GetCategoriesQueryHandler(IMapper mapper, IBaseRepository<Category> categoryRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<GetCategoriesQueryResponse> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAllAsync(cancellationToken);

            var getCategoriesList = _mapper.Map<List<GetCategoriesDto>>(categories);

            return new GetCategoriesQueryResponse(getCategoriesList);
        }
    }
}
