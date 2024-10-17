using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TinyUrl.RedirectionService.Infrastructure.Contracts.Options;
using TinyUrl.RedirectionService.Infrastructure.Entites;

namespace TinyUrl.RedirectionService.Infrastructure.Context
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _mongoDatabase;
        private const string URL_MAPPING_COLLECTION = "UrlsMapping";
        private const string USERS_LIMIT_COLLECTION = "UsersLimit";
        private readonly MongoDbOptions _options;

        public MongoDbContext(IMongoClient mongoClient, IOptions<MongoDbOptions> options)
        {
            _options = options.Value;
            _mongoDatabase = mongoClient.GetDatabase(_options.DatabaseName);
        }

        public IMongoCollection<UrlMapping> UrlMappings => _mongoDatabase.GetCollection<UrlMapping>(URL_MAPPING_COLLECTION);
        public IMongoCollection<UserLimit> UsersLimit => _mongoDatabase.GetCollection<UserLimit>(USERS_LIMIT_COLLECTION);
    }
}
