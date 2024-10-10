namespace TinyUrl.RedirectionService.Infrastructure.Services
{
    public interface IUrlMappingService
    {
        Task<string> GetLongUrlAsync(string shortUrl);
    }
}
