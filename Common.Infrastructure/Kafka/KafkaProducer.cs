using System.Threading.Tasks;

public class KafkaProducer : IKafkaProducer
{
    public Task SendAsync(IEvent @event, string topic)
    {
        throw new System.NotImplementedException();
    }
}