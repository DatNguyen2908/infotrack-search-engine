using Infotrack.Sales.SearchEngine.Application;
using Infotrack.Sales.SearchEngine.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace Infotrack.Sales.SearchEngine.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchEngineController : ControllerBase
    {
        private readonly ISearchEngineService _searchEngineService;

        public SearchEngineController(ISearchEngineService searchEngineService)
        {
            _searchEngineService = searchEngineService;

        }

        /// <summary>
        /// Get website rankings by given request.
        /// </summary>
        /// <param name="request">The request will contain keyword, url and the provider that used to search.</param>
        /// <returns></returns>
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("InfotrackWebApp")]
        public async Task<IActionResult> SearchAsync([FromQuery] SearchRankingsRequest request)
        {
            var searchResult = await _searchEngineService.SearchAsync(
                new SearchInputDto
                {
                    Keyword = request.Keyword,
                    Provider = request.Provider,
                    Url = request.Url
                });

            await _searchEngineService.StoreSearchResultsAsync(searchResult.SearchResultIndicies, request.Keyword, request.Url);

            return Ok(new SearchRankingsResponse
            {
                SearchResultIndicies = searchResult.SearchResultIndicies,
                SearchResultsText = searchResult.SearchResultsText,
                SearchResultsCount = searchResult.SearchResultsCount
            });
        }

        /// <summary>
        /// Get all search history rankings by given request.
        /// </summary>
        /// <param name="request">The request will contain keyword, url and the period of search dates.</param>
        /// <returns></returns>
        [HttpGet("histories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [EnableCors("InfotrackWebApp")]
        public async Task<IActionResult> GetSearchHistoriesAsync([FromQuery] SearchHistoriesRequest request)
        {
            var searchHistories = await _searchEngineService.GetSearchHistories(
                new SearchHistoryInputDto
                {
                    Keyword = request.Keyword,
                    Url = request.Url,
                    FromDate = request.FromDate,
                    ToDate = request.ToDate,
                });

            if (!searchHistories.Any())
            {
                return NotFound();
            }

            return Ok(searchHistories.Select(x => new SearchHistoryResponse
            {
                Keyword = x.Keyword,
                Url = x.Url,
                Rank = x.Rank,
                SearchDate = x.SearchDate
            }));
        }
    }
}