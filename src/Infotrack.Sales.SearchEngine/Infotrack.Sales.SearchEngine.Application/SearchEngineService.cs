using Infotrack.Sales.SearchEngine.Domain;
using Infotrack.Sales.SearchEngine.Domain.Repositories;

namespace Infotrack.Sales.SearchEngine.Application
{
    public class SearchEngineService : ISearchEngineService
    {
        private readonly IProviderSearchService _searchService;
        private readonly ISearchHistoryRepository _searchHistoryRepository;

        public SearchEngineService(IProviderSearchService searchService, ISearchHistoryRepository searchHistoryRepository)
        {
            _searchService = searchService;
            _searchHistoryRepository = searchHistoryRepository;
        }

        public async Task<SearchResultDto> SearchAsync(SearchInputDto input)
        {
            var result = await _searchService.SearchAsync(new SearchInput(input.Keyword, input.Url, input.Provider));

            return new SearchResultDto
            {
                SearchResultIndicies = result.SearchResultIndicies,
                SearchResultsText  = result.SearchResultsText,
                SearchResultsCount = result.SearchResultsCount
            };
        }

        public async Task StoreSearchResultsAsync(int[] searchResultIndicies, string keyword, string url)
        {
            if(!searchResultIndicies.Any()) 
            {
                return;
            }

            await _searchHistoryRepository.AddRangeAsync(
                searchResultIndicies.Select(x => new SearchHistory
                {
                    SearchDate = DateTime.UtcNow,
                    Keyword = keyword,
                    Rank = x,
                    Url = url
                }).ToArray());
        }

        public async Task<IReadOnlyCollection<SearchHistoryResultDto>> GetSearchHistories(SearchHistoryInputDto input)
        {
            var _searchHistories = await _searchHistoryRepository.GetAllAsync(input.Url, input.Keyword, input.FromDate, input.ToDate);

            return _searchHistories.Select(x => new SearchHistoryResultDto
            {
                Keyword = x.Keyword,
                Url = x.Url,
                Rank = x.Rank,
                SearchDate = x.SearchDate
            }).ToList();
        }
    }
}
