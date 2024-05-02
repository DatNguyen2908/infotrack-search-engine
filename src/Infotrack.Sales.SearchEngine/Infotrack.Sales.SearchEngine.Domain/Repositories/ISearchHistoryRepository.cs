namespace Infotrack.Sales.SearchEngine.Domain.Repositories
{
    public interface ISearchHistoryRepository
    {
        Task AddRangeAsync(SearchHistory[] searchHistories);

        Task<IReadOnlyCollection<SearchHistory>> GetAllAsync(string url, string keyword, DateTime? fromDate, DateTime? toDate);
    }
}