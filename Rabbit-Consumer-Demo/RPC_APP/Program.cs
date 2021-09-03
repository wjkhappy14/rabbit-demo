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
            channel.QueueDeclare(queue: "rpc_queue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            channel.BasicQos(0,
                             1,
                             false);
            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: "rpc_queue",
                                 autoAck: false,
                                 consumer: consumer);
            Console.WriteLine(" [x] Awaiting RPC requests");

            consumer.Received += (model, ea) =>
            {
                string result = string.Empty;

                byte[] body = ea.Body.ToArray();
                IBasicProperties props = ea.BasicProperties;
                IBasicProperties replyProps = channel.CreateBasicProperties();
                replyProps.CorrelationId = props.CorrelationId;
                try
                {
                    string message = Encoding.UTF8.GetString(body);
                    int n = int.Parse(message);
                    result = Fib(n).ToString();
                    Console.WriteLine($"fib({n})={result}");

                }
                catch (Exception e)
                {
                    Console.WriteLine(" [.] " + e.Message);
                    result = "";
                }
                finally
                {
                    byte[] responseBytes = Encoding.UTF8.GetBytes(result);
                    channel.BasicPublish(exchange: "",
                                         routingKey: props.ReplyTo,
                                         basicProperties: replyProps,
                                         body: responseBytes);
                    channel.BasicAck(deliveryTag: ea.DeliveryTag,
                                     multiple: false);
                }
            };
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }

    private static readonly Func<int, int> _fib = (int n) => n == 0 || n == 1 ? n : Fib(n - 1) + Fib(n - 2);
    public static Func<int, int> Fib => _fib;
}
