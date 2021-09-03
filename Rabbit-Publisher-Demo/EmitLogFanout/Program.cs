using System;
using RabbitMQ.Client;
using System.Text;

namespace EmitLogFanout
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
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                IBasicProperties props = channel.CreateBasicProperties();
                props.Persistent = true;
                channel.ExchangeDeclare(exchange: "logs-fanout",
                                        type: ExchangeType.Fanout,
                                        durable: true,
                                        autoDelete: false);

                string message = $"{Environment.MachineName} :现在时间是:{DateTime.Now}";
                byte[] body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "logs-fanout",
                                     routingKey: string.Empty,
                                     basicProperties: props,
                                     body: body);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"已发送 {message}");
            }
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}