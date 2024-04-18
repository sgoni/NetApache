using Banking.Account.Command.Application.Aggregates;
using Banking.CQRS.Core.Domain;
using Banking.CQRS.Core.Handlers;

namespace Banking.Account.Command.Infrastructure.KafkaEvents
{
    public class AccountEventSourcingHandler : EventSourcingHandler<AccountAggregate>
    {
        private readonly AccountEventStore _eventStore;

        public AccountEventSourcingHandler(AccountEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<AccountAggregate> GetById(string id)
        {
            var aggregate = new AccountAggregate();
            var events = await _eventStore.GetEvents(id);

            if (events != null && events.Any())
            {
                aggregate.ReplaceEvents(events);
                var latestVersion = events.Max(e => e.Version);
                aggregate.SetVersion(latestVersion);
            }

            return aggregate;
        }

        public async Task Save(AggregateRoot aggregate)
        {
            await _eventStore.SaveEvents(aggregate.Id, aggregate.GetUncommitedChanges(), aggregate.GetVersion());
            aggregate.MarkChangesAsCommited();
        }
    }
}
