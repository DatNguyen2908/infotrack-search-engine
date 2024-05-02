namespace Infotrack.Sales.SearchEngine.Application
{
    public interface ISearchEngineService
    {
        Task<SearchResultDto> SearchAsync(SearchInputDto input);

        Task StoreSearchResultsAsync(int[] searchResultIndicies, string keyword, string url);

        Task<IReadOnlyCollection<SearchHistoryResultDto>> GetSearchHistories(SearchHistoryInputDto input);
    }
}
