using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prescriptions.Api.Consumers
{
    public class PrescriptionsConsumer
    {
        private ConnectionFactory _factory { get; set; }
        private IConnection _connection { get; set; }
        private IModel _channel { get; set; }
        public PrescriptionsConsumer(string queue, Func<string, Task<bool>> fn)
        {
            _factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                Port = 5672
            };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: queue,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new RabbitMQ.Client.Events.EventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                await fn(message);
            };

            _channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);
        }
    }
}
