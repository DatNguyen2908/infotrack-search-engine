using Infotrack.Sales.SearchEngine.Constants;
using Infotrack.Sales.SearchEngine.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;

namespace Infotrack.Sales.SearchEngine.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> LoadHistories(string url, string keyword, DateTime? fromDate, DateTime? toDate)
        {
            using var _client = _httpClientFactory.CreateClient(ApiClientConstants.InfotrackApiClient);
            var response = await _client.GetAsync($"api/searchengine/histories?keyword={keyword}&url={url}&startDate={fromDate}&endDate={toDate}");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<IReadOnlyCollection<SearchHistoryModel>>(responseString);

                return Json(result);
            }

            return Json(new { errorMessage = "No histories found" });
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(SearchViewModel model)
        {
            using var _client = _httpClientFactory.CreateClient(ApiClientConstants.InfotrackApiClient);
            var response = await _client.GetAsync($"api/searchengine/search?keyword={model.Keyword}&url={model.Url}&provider={model.Provider}");
            if (response.IsSuccessStatusCode)
            {
                return await GenerateJsonMessage<SearchResultsModel>(model, response);
            }
            else
            {
                return await GenerateJsonMessage<ExceptionResponseModel>(model, response);
            }
        }

        private async Task<IActionResult> GenerateJsonMessage<T>(SearchViewModel model, HttpResponseMessage response) where T : class
        {
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<T>(responseString);

            return Json(result switch
            {
                SearchResultsModel successResult => $"Found: {model.Url} - {successResult.SearchResultsCount} times. Ranks: {successResult.SearchResultsText}",
                ExceptionResponseModel failResult => $"Error to search by {model.Provider} provider with keyword: {model.Keyword}. Message: {failResult.Description}",
                _ => string.Empty
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
