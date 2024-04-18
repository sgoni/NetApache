using Banking.Account.Command.Application.Aggregates;
using Banking.CQRS.Core.Handlers;
using MediatR;

namespace Banking.Account.Command.Application.Features.BankAccount.Commands.OpenAccount
{
    public class OpenAccountCommandHandler : IRequestHandler<OpenAccountCommand, bool>
    {
        private readonly EventSourcingHandler<AccountAggregate> _eventSourcingHandler;

        public OpenAccountCommandHandler(EventSourcingHandler<AccountAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;
        }

        public async Task<bool> Handle(OpenAccountCommand request, CancellationToken cancellationToken)
        {
            var aggregate = new AccountAggregate(request);
            await _eventSourcingHandler.Save(aggregate);
            return true;
        }
    }
}
