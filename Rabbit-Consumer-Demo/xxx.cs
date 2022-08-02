using RabbitMQ.Client;
using StackExchange.Redis;
using System;
using System.Diagnostics;
using System.Text;

namespace RabbitMQApp
{
    class Program
    {
        private static readonly ConnectionFactory factory = new ConnectionFactory()
        {
            Password = "rZKIBdbIaIHo6-9lF-vdNxTz37WxgWdm",
            Uri = new Uri("amqp://fjytyszq:rZKIBdbIaIHo6-9lF-vdNxTz37WxgWdm@dinosaur.rmq.cloudamqp.com/fjytyszq")
        };
        private static readonly IConnection connection = factory.CreateConnection();
        private static readonly ConnectionMultiplexer conn = ConnectionMultiplexer.Connect("127.0.0.1:6379");

        public static void Publish()
        {
            IDatabase db = conn.GetDatabase(1);
            PublishCast publishCast = new PublishCast();
            using (IModel channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: publishCast.Exchange, type: publishCast.XType);
                channel.QueueBind(publishCast.QueueName, publishCast.Exchange, publishCast.RoutingKey);

                Stopwatch watch = Stopwatch.StartNew();
                int x = 0;
                double elapsed = 0;
                do
                {
                    x++;
                    TickContent item = publishCast.New();
                    byte[] body = item.ToBytes();
                    // string message = futuresData;

                    string key = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    // string content = TickContent.New();
                    // byte[] body = Encoding.UTF8.GetBytes(message);
                    db.StringSet($"{key}:{x}", $"{item.TickTime}-{item.Content}");

                    channel.BasicPublish(exchange: publishCast.Exchange,
                                         routingKey: publishCast.RoutingKey,
                                         basicProperties: null,
                                         body: body);

                    Console.WriteLine($"{x}:Length={body.Length}");
                    elapsed = watch.Elapsed.TotalSeconds;
                }
                while (elapsed < 60);
                watch.Stop();
            }
        }
        static void Main(string[] args)
        {
            string futuresData = System.IO.File.ReadAllText("FuturesData.txt");
            Publish();
            Console.ReadLine();
        }
    }
}
