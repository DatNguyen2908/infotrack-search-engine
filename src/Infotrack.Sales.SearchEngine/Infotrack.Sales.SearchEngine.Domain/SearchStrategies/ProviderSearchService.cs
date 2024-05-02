namespace Infotrack.Sales.SearchEngine.Domain
{
    public class ProviderSearchService : IProviderSearchService
    {
        private readonly IEnumerable<ISearchProviderStrategy> _strategies;

        public ProviderSearchService(IEnumerable<ISearchProviderStrategy> strategies)
        {
            _strategies = strategies;
        }

        public async Task<SearchResult> SearchAsync(SearchInput input)
        {
            var searchStrategy = _strategies.FirstOrDefault(x => x.SearchProvider == input.Provider);

            if (searchStrategy == null)
            {
                throw new ProviderNotSupportedException(input.Provider);
            }

            return await searchStrategy.SearchAsync(input);
        }
    }
}
