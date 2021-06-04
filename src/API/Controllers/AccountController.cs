using System;
using System.Threading.Tasks;
using API.Application.Account.Commands.CreateAccount;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : ApiControllerBase
    {
        [HttpGet("{IBAN}", Name = "GetAccountByIBAN")]
        public async Task<ActionResult<string>> GetAccountByIBAN(string IBAN)
        {
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<AccountDto>> Create([FromForm] CreateAccountCommand command)
        {
            var dto = await Mediator.Send(command);
            return CreatedAtRoute("GetAccountByIBAN", new {dto.IBAN}, dto);
        }
    }
}