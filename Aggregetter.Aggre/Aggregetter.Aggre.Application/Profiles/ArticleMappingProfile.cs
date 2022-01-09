using Aggregetter.Aggre.Application.Features.Articles.Commands.CreateArticle;
using ArticleDetails = Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails;
using Articles = Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles;
using Aggregetter.Aggre.Domain.Entities;
using AutoMapper;

namespace Aggregetter.Aggre.Application.Profiles
{
    public sealed class ArticleMappingProfile : Profile
    {
        public ArticleMappingProfile()
        {
            #region GetArticles
            CreateMap<Article, Articles.ArticleDto>();
            #endregion GetArticles

            #region GetArticleDetails           
            CreateMap<Article, ArticleDetails.ArticleDetailsDto> ();
            CreateMap<Category, ArticleDetails.CategoryDto>();
            CreateMap<Language, ArticleDetails.LanguageDto>();
            CreateMap<Provider, ArticleDetails.ProviderDto>();
            #endregion GetArticleDetails

            #region CreateArticle
            CreateMap<Article, CreateArticleDto>().ReverseMap();
            CreateMap<Article, CreateArticleCommand>().ReverseMap();
            #endregion CreateArticle
        }
    }
}
