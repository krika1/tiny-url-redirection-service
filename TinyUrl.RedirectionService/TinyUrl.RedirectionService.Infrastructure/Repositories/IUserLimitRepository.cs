namespace TinyUrl.RedirectionService.Infrastructure.Repositories
{
    public interface IUserLimitRepository
    {
        Task<bool> IsUserLimitExceededAsync(int userId);
    }
}
