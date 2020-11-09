using System.Text;
using System.Text.Json;
using BancoBari.Infra.CrossCutting;
using BancoBari.MessageBroker.Send.v1.Messages;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace BancoBari.MessageBroker.Send.v1
{
    public class MessageProducer : IMessageProducer
    {
        private readonly MessageSettings _settings;
        public MessageProducer(IOptions<MessageSettings> options)
        {
            _settings = options.Value;
        }


        public void SendMessage<T>(string queueName, Message<T> message) where T: class
        {
            var factory = new ConnectionFactory() { HostName = _settings.HostName, UserName = _settings.UserName, Password = _settings.Password };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
        }
    }
}
