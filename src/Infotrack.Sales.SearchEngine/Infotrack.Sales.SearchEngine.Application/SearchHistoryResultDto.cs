namespace Infotrack.Sales.SearchEngine.Application
{
    public class SearchHistoryResultDto
    {
        public string Keyword { get; set; }

        public string Url { get; set; }

        public int Rank { get; set; }

        public DateTime SearchDate { get; set; }
    }
}
