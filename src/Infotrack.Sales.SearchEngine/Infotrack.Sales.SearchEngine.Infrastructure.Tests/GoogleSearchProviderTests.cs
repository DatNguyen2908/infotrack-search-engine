using Infotrack.Sales.SearchEngine.Constants;
using Infotrack.Sales.SearchEngine.Contracts;
using Infotrack.Sales.SearchEngine.Domain;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System.Net;

namespace Infotrack.Sales.SearchEngine.Infrastructure.Tests
{
    public class GoogleSearchProviderTests
    {
        private readonly Mock<HttpMessageHandler> _handlerMock;
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private readonly Mock<IOptions<GoogleSearchOptions>> _googleSearchOptionsMock;

        public GoogleSearchProviderTests()
        {
            _handlerMock = new();
            _httpClientFactoryMock = new();
            _googleSearchOptionsMock = new();
        }

        [Fact]
        public async Task GivenValidInput_WhenSendRequestToGoogleSuccessfully_ReturnsExpectedResults()
        {
            // Arrange
            var input = new SearchInput(Keyword: "land registry search", Url: "www.infotrack.co.uk", Provider: "Google");
            var expectedFirstFoundResult = 45;
            var expectedStringResponse = File.ReadAllText("SearchGoogleContentResult.html");

            SetupGoogleSearchOptionsMock();
            SetupHttpClientFactoryMock(input, HttpStatusCode.OK, expectedStringResponse);

            ISearchProviderStrategy searchProvider = new GoogleSearchProvider(_httpClientFactoryMock.Object, _googleSearchOptionsMock.Object);

            // Act
            var result = await searchProvider.SearchAsync(input);

            // Assert
            Assert.Collection<int>(result.SearchResultIndicies,
                firstResult =>
                {
                    Assert.IsType<int>(firstResult);
                    Assert.Equal(expectedFirstFoundResult, firstResult);
                });
        }

        [Fact]
        public async Task GivenValidInput_WhenFailedToSendRequestToGoogle_ThrowException()
        {
            // Arrange
            var input = new SearchInput(Keyword: "land registry search", Url: "www.infotrack.co.uk", Provider: "Google");
            SetupGoogleSearchOptionsMock();

            SetupHttpClientFactoryMock(input, HttpStatusCode.InternalServerError, string.Empty);

            ISearchProviderStrategy searchProvider = new GoogleSearchProvider(_httpClientFactoryMock.Object, _googleSearchOptionsMock.Object);

            // Act
            Func<Task<SearchResult>> result = () => searchProvider.SearchAsync(input);

            // Assert
            await Assert.ThrowsAsync<ProviderSearchException>(result);
        }

        private void SetupGoogleSearchOptionsMock()
        {
            _googleSearchOptionsMock.Setup(x => x.Value).Returns(new GoogleSearchOptions
            {
                ClassesContainUrl = "BNeawe UPmit AP7Wnd lRVwie",
                NumbersOfSearchResults = 100
            });
        }

        private void SetupHttpClientFactoryMock(SearchInput input, HttpStatusCode expectedStatusCode, string expectedStringResponse)
        {
            _handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri == new Uri($"http://www.google.com/search?num={_googleSearchOptionsMock.Object.Value.NumbersOfSearchResults}&q={Uri.EscapeDataString(input.Keyword)}")),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    Content = new StringContent(expectedStringResponse),
                    StatusCode = expectedStatusCode
                })
                .Verifiable();

            _httpClientFactoryMock.Setup(x => x.CreateClient(ApiClientConstants.GoogleApiClient))
                .Returns(new HttpClient(_handlerMock.Object)
                {
                    BaseAddress = new Uri("http://www.google.com/")
                });
        }
    }
}