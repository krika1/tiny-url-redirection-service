namespace TinyUrl.RedirectionService.Infrastructure.Services
{
    public interface ICacheService
    {
        object GetValue(string key);
        void SetValue(string key, object value);
    }
}
