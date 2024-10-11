namespace TinyUrl.RedirectionService.Infrastructure.Exceptions
{
    public class ShortUrlExpiredException : Exception
    {
        public ShortUrlExpiredException(string message) : base(message)
        {

        }
    }
}
