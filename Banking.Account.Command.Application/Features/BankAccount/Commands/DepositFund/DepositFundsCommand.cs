using MediatR;

namespace Banking.Account.Command.Application.Features.BankAccount.Commands.DepositFund
{
    internal class DepositFundsCommand : IRequest<bool>
    {
        public string Id { get; set; } = string.Empty;
        public double Amount { get; set; }
    }
}
