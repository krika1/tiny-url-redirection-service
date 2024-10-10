using TinyUrl.RedirectionService.Infrastructure.Entites;

namespace TinyUrl.RedirectionService.Infrastructure.Repositories
{
    public interface IUrlMappingRepository
    {
        Task<UrlMapping> GetUrlMapping(string shortUrl);
    }
}
