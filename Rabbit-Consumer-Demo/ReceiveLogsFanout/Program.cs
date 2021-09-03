using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

class Program
{
    public static void Main()
    {
        ConnectionFactory factory = new ConnectionFactory()
        {
            HostName = "117.50.40.186",
            UserName = "guest",
            Password = "guest",
            VirtualHost = "/"
        };
        using (IConnection connection = factory.CreateConnection())
        using (IModel channel = connection.CreateModel())
        {
            QueueDeclareOk queueName = channel.QueueDeclare(
                queue: "queue-logs-fanout",
                durable: true,
                exclusive: false,
                autoDelete: false);
            channel.QueueBind(
                queue: queueName,
                exchange: "logs-fanout",
                routingKey: string.Empty);
            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($" ’µΩ£∫{message}");
            };
            channel.BasicConsume(
                queue: queueName,
                autoAck: true,
                consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
