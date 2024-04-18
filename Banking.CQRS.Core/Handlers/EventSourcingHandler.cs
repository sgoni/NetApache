using Banking.CQRS.Core.Domain;

namespace Banking.CQRS.Core.Handlers
{
    public interface EventSourcingHandler<T>
    {
        Task Save(AggregateRoot aggregate);
        Task <T> GetById(string id);
    }
}
