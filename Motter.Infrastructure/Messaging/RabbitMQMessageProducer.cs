using Microsoft.Extensions.Configuration;
using Motter.Application.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Motter.Infrastructure.Messaging
{
    public class RabbitMQMessageProducer : IMessageProducer, IDisposable
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQMessageProducer(IConfiguration configuration)
        {
            var rabbitMQConfig = configuration.GetSection("RabbitMQ");
            _factory = new ConnectionFactory
            {
                HostName = rabbitMQConfig["HostName"],
                Port = int.Parse(rabbitMQConfig["Port"]),
                UserName = rabbitMQConfig["UserName"],
                Password = rabbitMQConfig["Password"]
            };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public Task PublishAsync<T>(string exchange, string routingKey, T message)
        {
            // Serialização da mensagem (JSON, por exemplo)
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            _channel.BasicPublish(exchange, routingKey, null, body);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }
}
