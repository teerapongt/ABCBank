using System;
using System.Threading.Tasks;
using API.Application.Account.Commands.CreateAccount;
using API.Application.Account.Queries.GetAccountByIBAN;
using Microsoft.AspNetCore.Mvc;
using AccountDto = API.Application.Account.Commands.CreateAccount.AccountDto;

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
        public async Task<ActionResult<AccountDto>> Create([FromForm] CreateAccountCommand command)
        {
            var dto = await Mediator.Send(command);
            return CreatedAtRoute("GetAccountByIBAN", new {dto.IBAN}, dto);
        }
    }
}