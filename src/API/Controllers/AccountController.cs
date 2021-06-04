using System.Collections.Generic;
using System.Threading.Tasks;
using API.Application.Account.Commands.CreateAccount;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<string>> Create([FromForm] CreateAccountCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}