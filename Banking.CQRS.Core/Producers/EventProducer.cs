using Banking.CQRS.Core.Events;

namespace Banking.CQRS.Core.Producers
{
    public interface EventProducer
    {
        void Produce(string topic, BaseEvent @event);
    }
}
