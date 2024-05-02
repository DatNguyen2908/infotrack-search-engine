namespace Infotrack.Sales.SearchEngine.Domain
{
    public class SearchHistory
    {
        public int Id { get; set; }

        public string Keyword { get; set; }

        public string Url { get; set; }

        public int Rank { get; set; }

        public DateTime SearchDate { get; set; }
    }
}
