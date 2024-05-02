using System.Net;
using System.Text.Json.Serialization;

namespace Infotrack.Sales.SearchEngine.WebApp.Models
{
    [Serializable]
    public class ExceptionResponseModel
    {
        [JsonPropertyName("statusCode")]
        public HttpStatusCode StatusCode { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
