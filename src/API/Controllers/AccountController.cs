using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Application.Account.Commands.Create;
using API.Application.Account.Commands.Deposit;
using API.Application.Account.Commands.Transfer;
using API.Application.Account.Queries.GetByIBAN;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : ApiControllerBase
    {
        [HttpGet("{IBAN}", Name = "GetByIBAN")]
        [ProducesResponseType(typeof(GetByIBANDto), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<GetByIBANDto>> GetByIBAN(string IBAN)
        {
            var dto = await Mediator.Send(new GetByIBANQuery {IBAN = IBAN});
            return Ok(dto);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<CreateDto>> Create([FromForm] CreateCommand command)
        {
            var dto = await Mediator.Send(command);
            return CreatedAtRoute("GetByIBAN", new {dto.IBAN}, dto);
        }

        [HttpPut("[action]/{IBAN}")]
        [ProducesResponseType(typeof(DepositDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<DepositDto>> Deposit(string IBAN, [FromForm] decimal amount)
        {
            var dto = await Mediator.Send(new DepositCommand {IBAN = IBAN, Amount = amount});
            return Ok(dto);
        }

        [HttpPut("[action]/{IBAN}")]
        [ProducesResponseType(typeof(TransferDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<TransferDto>> Transfer(string IBAN, [FromForm] string ToIBAN,
            [FromForm] decimal amount)
        {
            var dto = await Mediator.Send(new TransferCommand
                {FromIBAN = IBAN, ToIBAN = ToIBAN, Amount = amount});
            return Ok(dto);
        }
    }
}