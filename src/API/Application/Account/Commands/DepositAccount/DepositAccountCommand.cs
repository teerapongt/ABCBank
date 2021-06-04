using MediatR;

namespace API.Application.Account.Commands.DepositAccount
{
    public class DepositAccountCommand : IRequest<DepositAccountDto>
    {
        public string IBAN { get; set; }
        public decimal Deposit { get; set; }
    }
}