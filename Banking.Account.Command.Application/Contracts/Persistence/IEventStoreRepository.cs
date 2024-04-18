using Banking.Account.Command.Domain;

namespace Banking.Account.Command.Application.Contracts.Persistence
{
    public interface IEventStoreRepository : IMongoRepository<EventModel>
    {
        Task<IEnumerable<EventModel>> GetAllEventsAsync();
        Task<IEnumerable<EventModel>> FindByAggregateIdentifier(string agregateIdentifier);
    }
}
