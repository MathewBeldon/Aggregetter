using Aggregetter.Aggre.Application.Requests;
using Microsoft.AspNetCore.WebUtilities;
using System;

namespace Aggregetter.Aggre.Application.Services.UriService
{
    public class UriService : IUriService
    {
        private readonly string _baseUrl;
        public UriService(string baseUrl)
        {
            _baseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
        }

        public (Uri FirstUri, Uri PreviousUri, Uri NextUri, Uri LastUri)  GetPagedUris(PagedRequest pageRequest, string endpoint, int recordCount)
        {
            var _urlWithEndpoint = string.Concat(_baseUrl, "/", endpoint);

            var firstPage = QueryHelpers.AddQueryString(_urlWithEndpoint, "pageSize", pageRequest.PageSize.ToString());
            firstPage = QueryHelpers.AddQueryString(firstPage, "page", "1");

            string previousPage = string.Empty;
            if (pageRequest.Page > 1)
            {
                previousPage = QueryHelpers.AddQueryString(_urlWithEndpoint, "pageSize", (pageRequest.PageSize).ToString());
                previousPage = QueryHelpers.AddQueryString(previousPage, "page", (pageRequest.Page - 1).ToString());
            }

            var lastPageNumber = Math.Ceiling((double)recordCount / pageRequest.PageSize);
            string nextPage = string.Empty;
            if (pageRequest.Page + 1 <= lastPageNumber)
            {
                nextPage = QueryHelpers.AddQueryString(_urlWithEndpoint, "pageSize", (pageRequest.PageSize).ToString());
                nextPage = QueryHelpers.AddQueryString(nextPage, "page", (pageRequest.Page + 1).ToString());
            }

            var lastPage = QueryHelpers.AddQueryString(_urlWithEndpoint, "pageSize", pageRequest.PageSize.ToString());
            lastPage = QueryHelpers.AddQueryString(lastPage, "page", lastPageNumber.ToString());

            Uri firstUri = string.IsNullOrWhiteSpace(firstPage) ? null : new Uri(firstPage);
            Uri previousUri = string.IsNullOrWhiteSpace(previousPage) ? null : new Uri(previousPage);
            Uri nextUri = string.IsNullOrWhiteSpace(nextPage) ? null : new Uri(nextPage);
            Uri lastUri = string.IsNullOrWhiteSpace(lastPage) ? null : new Uri(lastPage);

            return (firstUri, previousUri, nextUri, lastUri);
        }
    }
}
