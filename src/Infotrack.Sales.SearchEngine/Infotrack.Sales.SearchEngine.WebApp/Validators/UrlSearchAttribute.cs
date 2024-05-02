using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Infotrack.Sales.SearchEngine.WebApp.Validators
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class UrlSearchAttribute : ValidationAttribute, IClientModelValidator
    {
        public override bool IsValid(object value)
        {
            if (value is string url)
            {
                if (url.StartsWith("www."))
                {
                    url = "http://" + url;
                }

                return Uri.TryCreate(url, UriKind.Absolute, out _);
            }

            return false;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            var errorMessage = FormatErrorMessage(context.ModelMetadata.GetDisplayName());
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-urlsearch", errorMessage);
        }

        private bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }
            attributes.Add(key, value);
            return true;
        }
    }
}
