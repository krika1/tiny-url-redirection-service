using TinyUrl.RedirectionService.Infrastructure.Repositories;
using TinyUrl.RedirectionService.Infrastructure.Services;

namespace TinyUrl.RedirectionService.Bussines.Services
{
    public class UrlMappingService : IUrlMappingService
    {
        private readonly IUrlMappingRepository _urlMappingRepository;

        public UrlMappingService(IUrlMappingRepository urlMappingRepository)
        {
            _urlMappingRepository = urlMappingRepository;
        }

        public async Task<string> GetLongUrlAsync(string shortUrl)
        {
            var urlMapping = await _urlMappingRepository.GetUrlMapping(shortUrl).ConfigureAwait(false);

            if (urlMapping is null) return null!;

            return urlMapping.LongUrl!;
        }
    }
}
