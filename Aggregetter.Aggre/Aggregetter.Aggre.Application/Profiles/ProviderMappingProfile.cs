using Aggregetter.Aggre.Application.Features.Providers.Queries.GetProviders;
using Aggregetter.Aggre.Domain.Entities;
using AutoMapper;

namespace Aggregetter.Aggre.Application.Profiles
{
    public sealed class ProviderMappingProfile : Profile
    {
        public ProviderMappingProfile()
        {
            CreateMap<Provider, GetProvidersDto>();
        }
    }
}
