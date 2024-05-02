namespace Infotrack.Sales.SearchEngine.Contracts
{
    /// <summary>
    /// Search website rankings request
    /// </summary>
    public class SearchRankingsRequest
    {
        /// <summary>
        /// Keyword that used to search website. e.g: land registry search
        /// </summary>
        public string Keyword { get; set; } = "land registry search";

        /// <summary>
        /// Url of website that used to search. e.g: www.infotrack.co.uk
        /// </summary>
        public string Url { get; set; } = "www.infotrack.co.uk";

        /// <summary>
        /// Search engine provider.
        /// Only support Google as of now. 
        /// </summary>
        public string Provider { get; set; } = "Google";
    }
}
