using Banking.Account.Command.Application.Aggregates;
using Banking.CQRS.Core.Handlers;
using MediatR;

namespace Banking.Account.Command.Application.Features.BankAccount.Commands.DepositFund
{
    public class DepositFundsCommandHandler : IRequestHandler<DepositFundsCommand, bool>
    {
        private readonly EventSourcingHandler<AccountAggregate> _eventSourcingHandler;

        public DepositFundsCommandHandler(EventSourcingHandler<AccountAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;
        }

        public async Task<bool> Handle(DepositFundsCommand request, CancellationToken cancellationToken)
        {
            var aggregate = await _eventSourcingHandler.GetById(request.Id);
            aggregate.DepositFunds(request.Amount);
            await _eventSourcingHandler.Save(aggregate);
            return true;
        }
    }
}
