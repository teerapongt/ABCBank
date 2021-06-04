using System;
using System.Threading.Tasks;
using API.Application.Account.Commands.CreateAccount;
using API.Application.Account.Commands.DepositAccount;
using API.Application.Account.Queries.GetAccountByIBAN;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : ApiControllerBase
    {
        [HttpGet("{IBAN}", Name = "GetAccountByIBAN")]
        public async Task<ActionResult<string>> GetAccountByIBAN(string IBAN)
        {
            var dto = await Mediator.Send(new GetAccountByIBANQuery {IBAN = IBAN});
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<CreateAccountDto>> Create([FromForm] CreateAccountCommand command)
        {
            var dto = await Mediator.Send(command);
            return CreatedAtRoute("GetAccountByIBAN", new {dto.IBAN}, dto);
        }

        [HttpPut("[action]/{IBAN}")]
        public async Task<ActionResult<CreateAccountDto>> Deposit(string IBAN, [FromForm] decimal deposit)
        {
            var dto = await Mediator.Send(new DepositAccountCommand {IBAN = IBAN, Deposit = deposit});
            return Accepted(dto);
        }
    }
}