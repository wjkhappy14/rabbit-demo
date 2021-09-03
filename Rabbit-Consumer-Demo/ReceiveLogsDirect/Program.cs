using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

internal class Program
{
    public static void Main(string[] args)
    {
        ConnectionFactory factory = new ConnectionFactory()
        {
            HostName = "117.50.40.186",
            UserName = "guest",
            Password = "guest",
            VirtualHost = "/"
        };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            QueueDeclareOk queueName = channel.QueueDeclare(queue: "123",
                                                          durable: true,
                                                          exclusive: false,
                                                          autoDelete: false);
            channel.QueueBind(queue: queueName,
                              exchange: "direct_logs",
                              routingKey: "123");

            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var routingKey = ea.RoutingKey;
                Console.WriteLine(" [x] Received '{0}':'{1}'", routingKey, message);
            };
            channel.BasicConsume(queue: "123", autoAck: true, consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
