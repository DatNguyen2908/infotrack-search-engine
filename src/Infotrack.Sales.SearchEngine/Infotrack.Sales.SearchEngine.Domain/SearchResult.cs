namespace Infotrack.Sales.SearchEngine.Domain
{
    public record SearchResult(int[] SearchResultIndicies)
    {
        public string SearchResultsText => string.Join(", ", SearchResultIndicies);

        public int SearchResultsCount => SearchResultIndicies.Count();
    }
}
