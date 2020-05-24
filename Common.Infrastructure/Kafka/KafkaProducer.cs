using System.Threading.Tasks;
using Common.Infrastructure.Contract;
using Microsoft.Extensions.Options;
using Confluent.Kafka;
using System;
using Newtonsoft.Json;

namespace Common.Infrastructure.Kafka
{
    public class KafkaProducer : IKafkaProducer
    {
        private readonly KafkaConfig config;

        public KafkaProducer(IOptions<KafkaConfig> config)
        {
            this.config = config.Value;
        }

        public async Task SendAsync(IEvent @event, string topic)
        {
            var producerConfig = new ProducerConfig 
            { 
                BootstrapServers = config.ServerName
            };

            using (var p = new ProducerBuilder<Null, string>(producerConfig).Build())
            {
                var dr = await p.ProduceAsync(topic, new Message<Null, string> { Value = GetMessage(@event)});
                Console.WriteLine($"Published message : '{dr.Value}'");
            }
        }

        private static string GetMessage(IEvent eventData)
        {
            var message = new KafkaMessage
            {
                Name = eventData.GetType().Name,
                EventData = JsonConvert.SerializeObject(eventData)
            };

            return JsonConvert.SerializeObject(message);
        }
    }
}
