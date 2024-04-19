using banking.Account.Query.Domain.Common;

namespace Banking.Account.Query.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : BaseDomainModel
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
        Task<T> AddEntity(T entity);
        Task<T> UpdateEntity(T entity);
        Task<T> DeleteEntity(T entity);
    }
}
