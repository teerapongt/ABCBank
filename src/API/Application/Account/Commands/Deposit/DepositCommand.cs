using MediatR;

namespace API.Application.Account.Commands.Deposit
{
    public class DepositCommand : IRequest<DepositDto>
    {
        public string IBAN { get; set; }
        public decimal Amount { get; set; }
    }
}