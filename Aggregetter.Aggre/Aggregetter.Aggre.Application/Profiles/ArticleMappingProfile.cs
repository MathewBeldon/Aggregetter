using Aggregetter.Aggre.Application.Features.Articles.Commands.CreateArticle;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles;
using Aggregetter.Aggre.Domain.Entities;
using AutoMapper;

namespace Aggregetter.Aggre.Application.Profiles
{
    public sealed class ArticleMappingProfile : Profile
    {
        public ArticleMappingProfile()
        {
            #region GetArticles
            CreateMap<Article, ArticleDto>();
            #endregion GetArticles

            #region GetArticleDetails           
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
