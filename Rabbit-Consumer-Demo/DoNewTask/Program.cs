using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

class Program
{
    public static void Main()
    {
        ConnectionFactory factory = new ConnectionFactory() { 
            HostName = "117.50.40.186",
            UserName = "guest",
            Password = "guest",
            VirtualHost = "/"
        };
        using (IConnection connection = factory.CreateConnection())
        using (IModel channel = connection.CreateModel())
        {
            channel.BasicQos(prefetchSize: 0,
                             prefetchCount: 1,
                             global: false);
            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: "task_queue",
                                 autoAck: false,
                                 consumer: consumer);
            consumer.Received += (model, e) =>
            {
                byte[] body = e.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);
                Console.WriteLine($" ’µΩ {message} Exchange={e.Exchange} DeliveryTag={e.DeliveryTag} RoutingKey={e.RoutingKey}");
                channel.BasicAck(deliveryTag: e.DeliveryTag,
                                 multiple: false);
            };
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
