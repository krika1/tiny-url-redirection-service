using MongoDB.Driver;
using TinyUrl.RedirectionService.Infrastructure.Context;
using TinyUrl.RedirectionService.Infrastructure.Repositories;

namespace TinyUrl.RedirectionService.Data.Repositories
{
    public class UserLimitRepository : IUserLimitRepository
    {
        private readonly MongoDbContext _dbContext;

        public UserLimitRepository(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> IsUserLimitExceededAsync(int userId)
        {
            var userLimitEntity = await _dbContext.UsersLimit.Find(us => us.UserId == userId).FirstOrDefaultAsync();

            return userLimitEntity.DailyCalls > 100;
        }
    }
}
