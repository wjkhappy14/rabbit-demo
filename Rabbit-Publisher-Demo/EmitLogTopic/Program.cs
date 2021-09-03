using System;
using RabbitMQ.Client;
using System.Text;
using System.Threading;

namespace EmitLogTopic
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
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                string routingKey = "anonymous.info";
                var props = channel.CreateBasicProperties();
                props.Persistent = true;
                channel.QueueDeclare(queue: "logs_queue",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                channel.QueueBind(queue: "logs_queue",
                                  exchange: "topic_logs",
                                  routingKey: routingKey);

                channel.ExchangeDeclare(exchange: "topic_logs",
                                        type: "topic",
                                        durable: true,
                                        autoDelete: false);
                int counter = 0;
                do
                {
                    Interlocked.Increment(ref counter);
                    string message = $"{counter} :Hello World! {DateTime.Now}";
                    byte[] body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "topic_logs", routingKey: routingKey, basicProperties: props, body: body);
                    Console.WriteLine(" [x] Sent '{0}':'{1}'", routingKey, message);
                }
                while (true);
            }
        }
    }
}