using System.Text.Json.Serialization;

namespace Infotrack.Sales.SearchEngine.WebApp.Models
{
    [Serializable]
    public class SearchResultsModel
    {
        [JsonPropertyName("searchResultIndicies")]
        public int[] SearchResultIndicies { get; set; }

        [JsonPropertyName("searchResultsText")]
        public string SearchResultsText { get; set; }

        [JsonPropertyName("searchResultsCount")]
        public int SearchResultsCount { get; set; }
    }
}
