using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PublisherConfirms
{

    [Serializable]
    public class TickContent
    {
        public TickContent()
        {

        }
        public static TickContent New()
        {
            return new TickContent() { Content = $"{DateTime.Now.Ticks}" };
        }

        public byte[] ToBytes()
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, this);
            return ms.ToArray();
        }
        public long TickTime => DateTimeOffset.Now.ToUnixTimeMilliseconds();
        public string Content { get; set; }
    }

    public class PublishCast
    {
        public string Exchange { get; set; } = $"gc.fanout";
        public string RoutingKey { get; set; } = "gc-1905";
        public string QueueName { get => RoutingKey; }

        public string XType => "fanout";

        public TickContent New() => TickContent.New();
    }
}
