using banking.Account.Query.Domain;
using MediatR;

namespace Banking.Account.Query.Application.Features.BankAccounts.Queries.FindAccountWithBalance
{
    public class FindAccountWithBalanceQuery : IRequest<IEnumerable<BankAccount>>
    {
        public string Identifier { get; set; } = string.Empty;
        public double Balance { get; set; }
        public string EqualityType { get; set; } = string.Empty;
    }
}
