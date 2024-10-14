using RabbitMQ.Client;
using System.Text;
using TinyUrl.RedirectionService.Infrastructure.Services;

namespace TinyUrl.RedirectionService.Bussines.Services
{
    public class RabbitMQService : IRabbitMQService, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQService()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "tinyUrl",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);
        }

        public void SendToQueue(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "",
                                   routingKey: "tinyUrl",
                                   basicProperties: null,
                                   body: body);
        }

        public void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
        }
    }
}
