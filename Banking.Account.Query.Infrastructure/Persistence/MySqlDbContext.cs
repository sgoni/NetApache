using banking.Account.Query.Domain;
using Microsoft.EntityFrameworkCore;

namespace Banking.Account.Query.Infrastructure.Persistence
{
    public class MySqlDbContext : DbContext
    {
        public DbSet<BankAccount>? BankAccounts { get; set; }

        public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options)
        {
        }
    }
}
