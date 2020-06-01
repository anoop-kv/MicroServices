using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Common.Infrastructure.Contract;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Kafka
{
    public class KafkaConsumer : BackgroundService
    {
        private readonly KafkaConfig config;
        private readonly IServiceProvider serviceProvider;

        public KafkaConsumer(IOptions<KafkaConfig> config, IServiceProvider serviceProvider)
        {
            this.config = config.Value;
            this.serviceProvider = serviceProvider;
        }

        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
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
                    var message = ParseAsKMessage(c.Consume(1000).Message);
                    var handlerType = GetHandlerType(message);

                    using (var scope = serviceProvider.CreateScope())
                    {
                        var handler = GetHandler(scope, handlerType);

                        await HandleMessageAsync(handler, message, cancellationToken);

                        c.Commit();
                    }
                }
            }

            return;
        }


        private Type GetHandlerType(KafkaMessage message)
        {
            return config.Handlers.TryGetValue(message.Name, out var handlerType) ? handlerType : null;
        }

        private KafkaMessage ParseAsKMessage(Message<Ignore, string> message)
        {
            return JsonConvert.DeserializeObject<KafkaMessage>(message.Value);
        }
            
        private async Task HandleMessageAsync(IServiceEventHandler handler, KafkaMessage integrationMessage, CancellationToken cancellationToken)
        {
            await handler.HandleAsync(JObject.Parse(integrationMessage.EventData), cancellationToken);
        }

        private IServiceEventHandler GetHandler(IServiceScope scope, Type handlerType)
        {
            var handler = scope.ServiceProvider.GetService(handlerType);
            return handler as IServiceEventHandler;
        }
    }
}
