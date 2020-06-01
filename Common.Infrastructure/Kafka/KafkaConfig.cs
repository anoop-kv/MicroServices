using System;
using System.Collections.Generic;
using Common.Infrastructure.Contract;

namespace Common.Infrastructure.Kafka
{
    public class KafkaConfig
    {
        public string ServerName { get; set; }

        public string Topic { get; set; }

        public IDictionary<string, Type> Handlers { get; set; } = new Dictionary<string, Type>();

        public KafkaConfig RegisterConsumer<TEvent, TEventHandler>()
            where TEvent : IEvent
            where TEventHandler : IServiceEventHandler
        {
            var eventName = typeof(TEvent).GetType().Name;
            Handlers[eventName] = typeof(TEventHandler);
            return this;
        }

    }
}