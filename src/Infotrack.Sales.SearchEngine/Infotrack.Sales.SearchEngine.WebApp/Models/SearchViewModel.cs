using Infotrack.Sales.SearchEngine.WebApp.Validators;
using System.ComponentModel.DataAnnotations;

namespace Infotrack.Sales.SearchEngine.WebApp.Models
{
    public class SearchViewModel
    {
        [Required]
        public string Keyword { get; set; }

        [Required, UrlSearch(ErrorMessage = "Please input valid Url!")]
        public string Url { get; set; }

        public string Provider { get; set; }
    }
}
