using MediatR;

namespace API.Application.Account.Commands.Transfer
{
    public class TransferCommand : IRequest<TransferDto>
    {
        public string FromIBAN { get; set; }
        public string ToIBAN { get; set; }
        public decimal Amount { get; set; }
    }
}