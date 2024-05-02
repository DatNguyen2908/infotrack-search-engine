namespace Infotrack.Sales.SearchEngine.Application
{
    public class SearchHistoryInputDto
    {
        public string Keyword { get; set; }

        public string Url { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}
