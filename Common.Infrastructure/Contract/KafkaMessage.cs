namespace Common.Infrastructure.Contract
{
    public class KafkaMessage 
    {
        public string Name { get; set; }

        public string EventData { get; set; }
    }
}
