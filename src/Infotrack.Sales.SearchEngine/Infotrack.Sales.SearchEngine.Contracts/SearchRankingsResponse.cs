namespace Infotrack.Sales.SearchEngine.Contracts
{
    /// <summary>
    /// Search website rankings response
    /// </summary>
    public class SearchRankingsResponse
    {
        /// <summary>
        /// Returns the list of response indices of the website
        /// </summary>
        public int[] SearchResultIndicies { get; set; }

        /// <summary>
        /// Returns the response indices which joins by comma
        /// </summary>
        public string SearchResultsText { get; set; }

        /// <summary>
        /// Returns number of times that the website returned in the response
        /// </summary>
        public int SearchResultsCount { get; set; }
    }
}
