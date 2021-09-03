using System;
using RabbitMQ.Client;
using System.Text;

namespace EmitLogDirect
{
    class Program
    {
        public static void Main(string[] args)
        {
            

               ConnectionFactory factory = new ConnectionFactory() { 
                HostName = "117.50.40.186", 
                UserName = "guest", 
                Password = "guest", 
                VirtualHost = "/" 
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //QueueDeclareOk queueName = channel.QueueDeclare(queue: "info",
                //                                                durable: true,
                //                                                exclusive: false,
                //                                                autoDelete: false);
                // channel.QueueBind(queue: queueName, exchange: "direct_logs", routingKey: "info");
                IBasicProperties props = channel.CreateBasicProperties();
                channel.ExchangeDeclare(exchange: "direct_logs",
                                        type: "direct");
                var message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "direct_logs",
                                     routingKey: "123",
                                     basicProperties: props,
                                     body: body);
                Console.WriteLine(" [x] Sent '{0}':'{1}'", "info", message);
            }
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}