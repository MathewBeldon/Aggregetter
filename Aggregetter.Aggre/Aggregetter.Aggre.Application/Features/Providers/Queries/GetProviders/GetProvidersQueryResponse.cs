using Aggregetter.Aggre.Application.Models.Base;
using System.Collections.Generic;

namespace Aggregetter.Aggre.Application.Features.Providers.Queries.GetProviders
{
    public sealed class GetProvidersQueryResponse : BaseResponse<List<GetProvidersDto>> 
    {
        public GetProvidersQueryResponse(List<GetProvidersDto> data = default(List<GetProvidersDto>)) : base(data) { }
    }
}
