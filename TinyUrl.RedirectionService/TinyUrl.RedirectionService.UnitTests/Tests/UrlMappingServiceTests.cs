using Moq;
using TinyUrl.RedirectionService.Bussines.Services;
using TinyUrl.RedirectionService.Infrastructure.Entites;
using TinyUrl.RedirectionService.Infrastructure.Repositories;

namespace TinyUrl.RedirectionService.UnitTests.Tests
{
    public class UrlMappingServiceTests
    {
        private readonly Mock<IUrlMappingRepository> _mockUrlMappingRepository;
        private readonly UrlMappingService _urlShortener; // Assuming the class is UrlShortener

        public UrlMappingServiceTests()
        {
            _mockUrlMappingRepository = new Mock<IUrlMappingRepository>();
            _urlShortener = new UrlMappingService(_mockUrlMappingRepository.Object);
        }

        [Fact]
        public async Task GetLongUrlAsync_ValidShortUrl_ReturnsLongUrl()
        {
            // Arrange
            var shortUrl = "abc123";
            var longUrl = "https://www.longurl.com";

            var urlMapping = new UrlMapping
            {
                ShortUrl = shortUrl,
                LongUrl = longUrl
            };

            _mockUrlMappingRepository
                .Setup(repo => repo.GetUrlMapping(shortUrl))
                .ReturnsAsync(urlMapping);

            // Act
            var result = await _urlShortener.GetLongUrlAsync(shortUrl);

            // Assert
            Assert.Equal(longUrl, result);
            _mockUrlMappingRepository.Verify(repo => repo.GetUrlMapping(shortUrl), Times.Once);
        }

        [Fact]
        public async Task GetLongUrlAsync_InvalidShortUrl_ReturnsNull()
        {
            // Arrange
            var shortUrl = "invalid123";

            _mockUrlMappingRepository
                .Setup(repo => repo.GetUrlMapping(shortUrl))
                .ReturnsAsync((UrlMapping)null); // Simulate null mapping

            // Act
            var result = await _urlShortener.GetLongUrlAsync(shortUrl);

            // Assert
            Assert.Null(result);
            _mockUrlMappingRepository.Verify(repo => repo.GetUrlMapping(shortUrl), Times.Once);
        }
    }
}
