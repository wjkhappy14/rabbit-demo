using System;
using RabbitMQ.Client;
using System.Text;
using System.Threading;

namespace DoNewTask
{
    class Program
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
                channel.ExchangeDeclare(
                    exchange: "hello-exchange",
                    type: ExchangeType.Direct,
                    durable: true
                    );
                IBasicProperties properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                int counter = 0;
                do
                {
                    Interlocked.Increment(ref counter);
                    string message = $"{counter} :Hello World! {DateTime.Now}";
                    byte[] body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(
                        exchange: "hello-exchange",
                        routingKey: "task_queue",
                        basicProperties: properties,
                        body: body
                        );
                    Console.WriteLine("发送 {0}", message);
                    Thread.Sleep(1 * 1000);
                }
                while (1 > 0);
            }
        }
    }
}