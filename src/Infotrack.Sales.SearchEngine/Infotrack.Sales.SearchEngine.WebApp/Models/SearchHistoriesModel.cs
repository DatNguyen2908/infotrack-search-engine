using System.Text.Json.Serialization;

namespace Infotrack.Sales.SearchEngine.WebApp.Models
{
    public class SearchHistoryModel
    {
        [JsonPropertyName("keyword")]
        public string Keyword { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("rank")]
        public int Rank { get; set; }

        [JsonPropertyName("searchDate")]
        public DateTime SearchDate { get; set; }
    }
}
