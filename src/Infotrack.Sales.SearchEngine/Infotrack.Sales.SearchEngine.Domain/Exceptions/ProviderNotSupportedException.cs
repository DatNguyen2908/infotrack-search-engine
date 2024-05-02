namespace Infotrack.Sales.SearchEngine.Domain
{
    public class ProviderNotSupportedException : Exception
    {
        public ProviderNotSupportedException(string provider) : base($"Provider {provider} is not supported to use as of now!")
        {
        }
    }
}
