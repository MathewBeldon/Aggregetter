using Aggregetter.Aggre.Application.Features.Articles.Commands.CreateArticle;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles;
using Aggregetter.Aggre.Application.Features.Search.Queries.GetArticleSearchResults;
using Aggregetter.Aggre.Domain.Entities;
using AutoMapper;
using ArticleDetails = Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails;

namespace Aggregetter.Aggre.Application.Profiles
{
    public sealed class ArticleMappingProfile : Profile
    {
        public ArticleMappingProfile()
        {
            #region GetArticles
            CreateMap<Article, GetArticlesDto>();
            CreateMap<Category, GetArticlesCategoryDto>();
            CreateMap<Provider, GetArticlesProviderDto>();
            #endregion GetArticles

            #region GetArticleDetails           
            CreateMap<Article, ArticleDetails.GetArticleDetailsDto> ();
            CreateMap<Category, ArticleDetails.GetArticleDetailsCategoryDto>();
            CreateMap<Language, ArticleDetails.GetArticleDetailsLanguageDto>();
            CreateMap<Provider, ArticleDetails.GetArticleDetailsProviderDto>();
            #endregion GetArticleDetails

            #region CreateArticle
            CreateMap<Article, CreateArticleDto>().ReverseMap();
            CreateMap<Article, CreateArticleCommand>().ReverseMap();
            #endregion CreateArticle

            #region GetArticleSearchResults
            CreateMap<Article, GetArticleSearchResultsDto>();
            #endregion GetArticleSearchResults
        }
    }    
}
