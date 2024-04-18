using Banking.Account.Command.Application.Models;
using Banking.CQRS.Core.Events;
using Banking.CQRS.Core.Producers;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Banking.Account.Command.Infrastructure.KafkaEvents
{
    public class AccountEventProducer : EventProducer
    {
        public KafkaSettings _kakfaSettings;

        public AccountEventProducer(IOptions<KafkaSettings> kakfaSettings)
        {
            _kakfaSettings = kakfaSettings.Value;
        }

        public void Produce(string topic, BaseEvent @event)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = $"{_kakfaSettings.Hostname}:{_kakfaSettings.Port}"
            };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                var classEvent = @event.GetType();
                string value = JsonConvert.SerializeObject(@event);
                var message = new Confluent.Kafka.Message<Null, string> { Value = value };
                producer.ProduceAsync(topic, message).GetAwaiter().GetResult();
            }
        }
    }
}
