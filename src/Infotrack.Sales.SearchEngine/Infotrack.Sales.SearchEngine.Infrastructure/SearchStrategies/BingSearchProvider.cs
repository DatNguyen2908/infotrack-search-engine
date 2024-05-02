using Infotrack.Sales.SearchEngine.Constants;
using Infotrack.Sales.SearchEngine.Domain;


namespace Infotrack.Sales.SearchEngine.Infrastructure
{
    public sealed class BingSearchProvider : ISearchProviderStrategy
    {
        public string SearchProvider => ProviderConstants.Bing;

        private readonly IHttpClientFactory _httpClientFactory;

        public BingSearchProvider(
            IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public Task<SearchResult> SearchAsync(SearchInput input)
        {
            // TODO: Implements this feature later once CTO of Infotrack hired me to complete it! :)
            throw new ProviderNotSupportedException(SearchProvider);
        }
    }
}
