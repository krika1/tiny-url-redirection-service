using TinyUrl.RedirectionService.Infrastructure.Common;
using TinyUrl.RedirectionService.Infrastructure.Entites;
using TinyUrl.RedirectionService.Infrastructure.Exceptions;
using TinyUrl.RedirectionService.Infrastructure.Repositories;
using TinyUrl.RedirectionService.Infrastructure.Services;

namespace TinyUrl.RedirectionService.Bussines.Services
{
    public class UrlMappingService : IUrlMappingService
    {
        private readonly IUrlMappingRepository _urlMappingRepository;
        private readonly ICacheService _cacheService;

        public UrlMappingService(IUrlMappingRepository urlMappingRepository, ICacheService cacheService)
        {
            _urlMappingRepository = urlMappingRepository;
            _cacheService = cacheService;
        }

        public async Task<string> GetLongUrlAsync(string shortUrl)
        {
            var urlMapping = GetFromCache(shortUrl);

            if (urlMapping is null)
            {
                urlMapping = await _urlMappingRepository.GetUrlMapping(shortUrl).ConfigureAwait(false);

                if (urlMapping is null) return null!;

                _cacheService.SetValue(shortUrl, urlMapping);
            }

            CheckExpiringDate(urlMapping!);

            return urlMapping.LongUrl!;
        }

        private UrlMapping? GetFromCache(string shortUrl)
        {
            var urlMapping = _cacheService.GetValue(shortUrl);

            return urlMapping as UrlMapping;
        }

        private void CheckExpiringDate(UrlMapping mapping)
        {
            if (mapping.ExpirationDate <= DateTime.Now)
            {
                throw new ShortUrlExpiredException(ErrorMessages.ShortUrlExpiredErrorMessage);
            }
        }
    }
}
