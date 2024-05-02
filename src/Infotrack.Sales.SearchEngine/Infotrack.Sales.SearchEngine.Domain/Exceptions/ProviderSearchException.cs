namespace Infotrack.Sales.SearchEngine.Domain
{
    public class ProviderSearchException : InvalidOperationException
    {
        public ProviderSearchException(string provider, Exception innerException) : base($"Calling provider {provider} failed due to '{innerException.Message}'", innerException)
        {
        }
    }
}
