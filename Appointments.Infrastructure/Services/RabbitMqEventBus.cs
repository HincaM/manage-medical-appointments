using Appointments.Application.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Appointments.Infrastructure.Services
{
    public sealed class RabbitMqEventBus : IEventBus
    {
        public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                Port = 5672
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    string queue = @event.Queue;
                    channel.QueueDeclare(
                        queue: queue,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

                    @event.Queue = "";

                    var message = JsonConvert.SerializeObject(@event);
                    var body = System.Text.Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: queue,
                        body: body
                    );
                }
            }
        }
    }
}
