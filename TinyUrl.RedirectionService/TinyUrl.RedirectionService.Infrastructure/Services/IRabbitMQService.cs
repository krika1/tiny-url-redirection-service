namespace TinyUrl.RedirectionService.Infrastructure.Services
{
    public interface IRabbitMQService
    {
        void SendToQueue(string message);
    }
}
