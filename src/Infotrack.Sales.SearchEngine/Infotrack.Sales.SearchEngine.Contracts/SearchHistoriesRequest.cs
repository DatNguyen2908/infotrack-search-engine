namespace Infotrack.Sales.SearchEngine.Contracts
{
    /// <summary>
    /// Search website rankings request
    /// </summary>
    public class SearchHistoriesRequest
    {
        public string Keyword { get; set; }

        public string Url { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}
