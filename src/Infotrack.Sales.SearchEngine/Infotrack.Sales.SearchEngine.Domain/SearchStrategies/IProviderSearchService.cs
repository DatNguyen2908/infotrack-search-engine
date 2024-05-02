namespace Infotrack.Sales.SearchEngine.Domain
{
    public interface IProviderSearchService
    {
        Task<SearchResult> SearchAsync(SearchInput input);
    }
}
