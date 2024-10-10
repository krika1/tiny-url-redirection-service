using MongoDB.Driver;
using TinyUrl.RedirectionService.Infrastructure.Context;
using TinyUrl.RedirectionService.Infrastructure.Entites;
using TinyUrl.RedirectionService.Infrastructure.Repositories;

namespace TinyUrl.RedirectionService.Data.Repositories
{
    public class UrlMappingRepository : IUrlMappingRepository
    {
        private readonly MongoDbContext _dbContext;

        public UrlMappingRepository(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UrlMapping> GetUrlMapping(string shortUrl)
        {
            var urlMapping = await _dbContext.UrlMappings
                .Find(um => um.ShortUrl!.Contains(shortUrl))
                .FirstOrDefaultAsync();

            return urlMapping;
        }
    }
}
