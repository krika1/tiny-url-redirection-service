namespace TinyUrl.RedirectionService.Infrastructure.Exceptions
{
    public class DailyCallsExceededException : Exception
    {
        public DailyCallsExceededException(string message) : base(message)
        {

        }
    }
}
