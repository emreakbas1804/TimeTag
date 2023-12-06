using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TimeTag.Application.Abstractions;

namespace TimeTag.Infrastructure.Concretes
{
    public class RabbitMqService : IRabbitMqService
    {
        
        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            var connection = factory.CreateConnection();
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("emailService", exclusive: false, autoDelete: false);
                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);
                channel.BasicPublish(exchange: "", routingKey: "emailService", body: body);
            }
        }
     
    }
}