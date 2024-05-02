using System.Net;

namespace Infotrack.Sales.SearchEngine.Contracts
{
    public class ExceptionResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Description { get; set; }
    }
}
