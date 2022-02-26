using Aggregetter.Aggre.Application.Models.Base;
using System.Collections.Generic;

namespace Aggregetter.Aggre.Application.Features.Categories.Queries.GetCategories
{
    public sealed class GetProvidersQueryREsponse : ContentResponse<List<GetCategoriesDto>>
    {
        public GetProvidersQueryREsponse(List<GetCategoriesDto> data = default(List<GetCategoriesDto>)) : base(data) { }
    }
}