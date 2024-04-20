using banking.Account.Query.Domain;
using Banking.Account.Query.Application.Contracts.Persistence;
using Banking.Account.Query.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Banking.Account.Query.Infrastructure.Repositories
{
    public class BankAccountRepository : RepositoryBase<BankAccount>, IBankAccountRepository
    {
        public BankAccountRepository(MySqlDbContext dbContext) : base(dbContext)
        {
        }

        public async Task DeleteByIdentifier(string identifier)
        {
            var bankAccount = await _dbContext.BankAccounts!.Where(x => x.Identifier == identifier).FirstOrDefaultAsync();

            if (bankAccount != null)
            {
                throw new Exception($"No se puedo eliminar la cuenta bancaria con el id {identifier}");
            }

            _dbContext.BankAccounts!.Remove(bankAccount);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DepositBankAccountByIdentifier(BankAccount bankAccount)
        {
            var account = await _dbContext.BankAccounts!.Where(x => x.Identifier == bankAccount.Identifier).FirstOrDefaultAsync();

            if (account != null)
            {
                throw new Exception($"No se puedo actualizar en MySQL la cuenta bancaria con el id {bankAccount.Identifier}");
            }

            account.Balance += bankAccount.Balance;
            await UpdateAsync(account);
        }

        public async Task<IEnumerable<BankAccount>> FindByAccountHolder(string accountHolder)
        {
            return await _dbContext.BankAccounts!.Where(x => x.AccountHolder == accountHolder).ToListAsync();
        }

        public async Task<BankAccount> FindByAccountIdentifier(string identifier)
        {
            return await _dbContext.BankAccounts!.Where(x => x.Identifier == identifier).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BankAccount>> FindByBalanceGreaterThan(double balance)
        {
            return await _dbContext.BankAccounts!.Where(x => x.Balance > balance).ToListAsync();
        }

        public async Task<IEnumerable<BankAccount>> FindByBalanceLessThan(double balance)
        {
            return await _dbContext.BankAccounts!.Where(x => x.Balance < balance).ToListAsync();
        }

        public async Task WithdrawnBankAccountByIdentifier(BankAccount bankAccount)
        {
            var account = await _dbContext.BankAccounts!.Where(x => x.Identifier == bankAccount.Identifier).FirstOrDefaultAsync();

            if (account != null)
            {
                throw new Exception($"No se puedo actualizar en MySQL la cuenta bancaria con el id {bankAccount.Identifier}");
            }

            if (account.Balance < bankAccount.Balance)
            {
                throw new Exception($"El balance de la cuenta es menor que el deseo que desea retirar para {bankAccount.Id}");
            }

            account.Balance -= bankAccount.Balance;
            await UpdateAsync(account);
        }
    }
}
