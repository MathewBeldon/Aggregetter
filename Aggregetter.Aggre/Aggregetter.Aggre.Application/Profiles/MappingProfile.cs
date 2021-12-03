using Aggregetter.Aggre.Application.Features.Articles.Commands.CreateArticle;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleList;
using Aggregetter.Aggre.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region GetArticleDetails

            CreateMap<Article, ArticlePagedListVm>();
            CreateMap<Article, ArticleDetailsVm>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Language, LanguageDto>();
            CreateMap<Provider, ProviderDto>();

            #endregion GetArticleDetails

            #region CreateArticle

            CreateMap<Article, CreateArticleDto>().ReverseMap();
            CreateMap<Article, CreateArticleCommand>().ReverseMap();

            #endregion CreateArticle

        }
    }
}
