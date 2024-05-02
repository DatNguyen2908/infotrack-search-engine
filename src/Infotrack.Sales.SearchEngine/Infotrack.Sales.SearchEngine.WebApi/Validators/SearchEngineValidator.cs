using FluentValidation;
using Infotrack.Sales.SearchEngine.Constants;
using Infotrack.Sales.SearchEngine.Contracts;

namespace Infotrack.Sales.SearchEngine.WebApi.Validators
{
    public class SearchEngineValidator : AbstractValidator<SearchRankingsRequest>
    {
        private readonly string[] _validProviders = [ProviderConstants.Google];

        public SearchEngineValidator()
        {
            RuleFor(x => x.Provider)
                .NotEmpty()
                .Must(provider => _validProviders.Contains(provider))
                .WithMessage($"Currently system only support to search with providers: {string.Join(", ", _validProviders)}!");

            RuleFor(x => x.Url)
                .NotEmpty()
                .Must(BeValidUrl)
                .WithMessage("Your input URL is not valid!");

            RuleFor(x => x.Keyword)
                .NotEmpty()
                .WithMessage("Keyword cannot be null or empty!");
        }

        private bool BeValidUrl(string url)
        {
            if(url.StartsWith("www."))
            {
                url = "http://" + url;
            }

            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }
}
