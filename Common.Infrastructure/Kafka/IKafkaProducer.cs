using System.Threading.Tasks;

public interface IKafkaProducer
{
    Task SendAsync(IEvent @event, string topic);
}