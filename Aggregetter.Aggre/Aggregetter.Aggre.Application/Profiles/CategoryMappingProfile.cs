using Aggregetter.Aggre.Application.Features.Categories.Queries.GetCategories;
using Aggregetter.Aggre.Domain.Entities;
using AutoMapper;

namespace Aggregetter.Aggre.Application.Profiles
{
    public sealed class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, GetCategoriesDto>();
        }
    }
}
