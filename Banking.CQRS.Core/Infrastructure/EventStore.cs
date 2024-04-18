using Banking.CQRS.Core.Events;

namespace Banking.CQRS.Core.Infrastructure
{
    public interface EventStore
    {
        Task SaveEvents(string aggregateId, IEnumerable<BaseEvent> events, int expectedVersion);

        Task<List<BaseEvent>> GetEvents(string aggregateId);
    }
}
