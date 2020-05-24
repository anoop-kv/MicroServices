using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace Common.Infrastructure.Kafka
{
    public class KafkaConsumer : BackgroundService
    {
        private readonly KafkaConfig config;

        public KafkaConsumer(IOptions<KafkaConfig> config)
        {
            this.config = config.Value;
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = config.ServerName
            };

            using (var c = new ConsumerBuilder<Ignore, string>(consumerConfig).Build())
            {
                c.Subscribe(config.Topic);
                while (!cancellationToken.IsCancellationRequested)
                {
                    var result = c.Consume(1000);
                    Console.WriteLine($"Received message '{result.Message.Value}'");
                }
            }

            return Task.CompletedTask;
        }
    }
}
