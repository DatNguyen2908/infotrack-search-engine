
using Infotrack.Sales.SearchEngine.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Infotrack.Sales.SearchEngine.Domain.Repositories
{
    public class SearchHistoryRepository : ISearchHistoryRepository
    {
        private readonly SearchEngineContext _context;

        public SearchHistoryRepository(SearchEngineContext context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(SearchHistory[] searchHistories)
        {
            _context.SearchHistories.AddRange(searchHistories);

            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<SearchHistory>> GetAllAsync(string url, string keyword, DateTime? fromDate, DateTime? toDate)
        {
            var _searchHistories = _context.SearchHistories.AsQueryable();

            if(!string.IsNullOrEmpty(url))
            {
                _searchHistories = _searchHistories.Where(x => x.Url == url);
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                _searchHistories = _searchHistories.Where(x => x.Keyword == keyword);
            }

            if (fromDate.HasValue && toDate.HasValue)
            {
                _searchHistories = _searchHistories.Where(x => x.SearchDate >= fromDate && x.SearchDate <= toDate);
            }

            return await _searchHistories.ToListAsync();
        }
    }
}