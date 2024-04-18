using Banking.Account.Command.Application.Features.BankAccount.Commands.CloseAccount;
using Banking.Account.Command.Application.Features.BankAccount.Commands.DepositFund;
using Banking.Account.Command.Application.Features.BankAccount.Commands.OpenAccount;
using Banking.Account.Command.Application.Features.BankAccount.Commands.WithdrawnFund;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Banking.Account.Command.Api.Controllers
{
    /// <summary>
    /// BankAccountOperationsAccount Controller.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BankAccountOperationsAccountController : Controller
    {
        private readonly IMediator _mediator;

        public BankAccountOperationsAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("OpenAccount", Name = "OpenAccount")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> OpenAccount([FromBody] OpenAccountCommand command)
        {
            var id = Guid.NewGuid().ToString();
            command.Id = id;
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("CloseAccount/{id}", Name = "CloseAccount")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<bool>> CloseAccount(string id)
        {
            CloseAccountCommand command = new CloseAccountCommand();
            command.Id = id;
            return await _mediator.Send(command);
        }

        [HttpPut("DepositFund/{id}", Name = "DepositFund")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<bool>> DepositFunds(string id, [FromBody] DepositFundsCommand command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        [HttpPut("WithdrawnFund/{id}", Name = "WithdrawnFund")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<bool>> WithdrawnFund(string id, [FromBody] WithdrawFundsCommand command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }
    }
}
