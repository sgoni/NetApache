using Banking.Account.Command.Application.Contracts.Persistence;
using Banking.Account.Command.Application.Models;
using Banking.Account.Command.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Banking.Account.Command.Infrastructure.Repositories
{
    public class EventStoreRepository : MongoRepository<EventModel>, IEventStoreRepository
    {
        public EventStoreRepository(IOptions<MongoSettings> options) : base(options)
        {
        }

        public async Task<IEnumerable<EventModel>> FindByAggregateIdentifier(string agregateIdentifier)
        {
            var filter = Builders<EventModel>.Filter.Eq(doc => doc.AggregateIdentifier, agregateIdentifier);
            return await _collection.Find(filter).ToListAsync();

        }

        public Task<IEnumerable<EventModel>> GetAllEventsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
