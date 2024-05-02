using Microsoft.Extensions.Options;
using Infotrack.Sales.SearchEngine.Domain;
using Infotrack.Sales.SearchEngine.Constants;
using Infotrack.Sales.SearchEngine.Contracts;
using System.Text.RegularExpressions;

namespace Infotrack.Sales.SearchEngine.Infrastructure
{
    public sealed class GoogleSearchProvider : ISearchProviderStrategy
    {
        public string SearchProvider => ProviderConstants.Google;

        private readonly GoogleSearchOptions _googleSearchOptions;
        private readonly IHttpClientFactory _httpClientFactory;

        public GoogleSearchProvider(
            IHttpClientFactory httpClientFactory,
            IOptions<GoogleSearchOptions> googleSearchOptions)
        {
            _googleSearchOptions = googleSearchOptions.Value;
            this._httpClientFactory = httpClientFactory;
        }

        public async Task<SearchResult> SearchAsync(SearchInput input)
        {
            try
            {
                using var httpClient = _httpClientFactory.CreateClient(ApiClientConstants.GoogleApiClient);

                var response = await httpClient.GetAsync(
                    $"search?num={_googleSearchOptions.NumbersOfSearchResults}&q={Uri.EscapeDataString(input.Keyword)}");

                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();

                var result = Regex.Matches(
                    responseString, 
                    @$"<div class=""{_googleSearchOptions.ClassesContainUrl}"">(.*?)<\/div>")
                    .Select((x, index) => new { Url = x.Groups[1].Value.Split(" ")[0], Index = index })
                    .Where(x => x.Url.Contains(input.Url))
                    .Select(x => x.Index)
                    .ToArray();

                return new SearchResult(result);
            }
            catch (HttpRequestException ex)
            {
                throw new ProviderSearchException(SearchProvider, ex);
            }
        }
    }

}
