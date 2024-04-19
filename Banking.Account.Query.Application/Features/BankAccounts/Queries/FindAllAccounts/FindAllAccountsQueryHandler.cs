using banking.Account.Query.Domain;
using Banking.Account.Query.Application.Contracts.Persistence;
using MediatR;

namespace Banking.Account.Query.Application.Features.BankAccounts.Queries.FindAllAccounts
{
    public class FindAllAccountsQueryHandler : IRequestHandler<FindAllAccountsQuery, IEnumerable<BankAccount>>
    {
        private readonly IBankAccountRepository _bankAccountRepository;

        public FindAllAccountsQueryHandler(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task<IEnumerable<BankAccount>> Handle(FindAllAccountsQuery request, CancellationToken cancellationToken)
        {
            return await _bankAccountRepository.GetAllAsync();
        }
    }
}
