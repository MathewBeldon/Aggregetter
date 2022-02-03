using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Providers.Queries.GetProviders
{
    public sealed class GetProvidersQueryHandler : IRequestHandler<GetProvidersQuery, GetProvidersQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Provider> _providerRepository;

        public GetProvidersQueryHandler(IMapper mapper, IBaseRepository<Provider> providerRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _providerRepository = providerRepository ?? throw new ArgumentNullException(nameof(providerRepository));
        }

        public async Task<GetProvidersQueryResponse> Handle(GetProvidersQuery request, CancellationToken cancellationToken)
        {
            var providerEntities = await _providerRepository.GetAllAsync(cancellationToken);

            var providerDtos = _mapper.Map<List<GetProvidersDto>>(providerEntities);

            return new GetProvidersQueryResponse(providerDtos);
        }
    }
}
