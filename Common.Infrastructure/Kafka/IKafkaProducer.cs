using System.Threading.Tasks;
using Common.Infrastructure.Contract;

namespace Common.Infrastructure.Kafka
{
    public interface IKafkaProducer
    {
        Task SendAsync(IEvent @event, string topic);
    }
}
