namespace Infotrack.Sales.SearchEngine.Domain
{
    public interface ISearchProviderStrategy
    {
        Task<SearchResult> SearchAsync(SearchInput input);

        string SearchProvider { get; }
    }
}
