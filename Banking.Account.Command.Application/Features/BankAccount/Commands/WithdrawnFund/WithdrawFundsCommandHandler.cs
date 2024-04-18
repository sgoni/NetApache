using Banking.Account.Command.Application.Aggregates;
using Banking.CQRS.Core.Handlers;
using MediatR;

namespace Banking.Account.Command.Application.Features.BankAccount.Commands.WithdrawnFund
{
    public class WithdrawFundsCommandHandler:IRequestHandler<WithdrawFundsCommand, bool>
    {
        private readonly EventSourcingHandler<AccountAggregate> _eventSourcingHandler;

        public WithdrawFundsCommandHandler(EventSourcingHandler<AccountAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;
        }

        public async Task<bool> Handle(WithdrawFundsCommand request, CancellationToken cancellationToken)
        {
            var aggregate = await _eventSourcingHandler.GetById(request.Id);
            aggregate.WithdrawFunds(request.Amount);
            await _eventSourcingHandler.Save(aggregate);
            return true;
        }
    }
}
