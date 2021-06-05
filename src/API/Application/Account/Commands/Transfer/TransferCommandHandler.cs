using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Account.Commands.Transfer
{
    public class TransferCommandHandler : IRequestHandler<TransferCommand, TransferDto>
    {
        private readonly IABCBankDbContext _context;

        public TransferCommandHandler(IABCBankDbContext context)
        {
            _context = context;
        }

        public async Task<TransferDto> Handle(TransferCommand request, CancellationToken cancellationToken)
        {
            var from = await _context.Accounts
                .AsNoTracking()
                .Where(w => Equals(w.IBAN, request.FromIBAN))
                .SingleAsync(cancellationToken);

            var to = await _context.Accounts
                .AsNoTracking()
                .Where(w => Equals(w.IBAN, request.ToIBAN))
                .SingleAsync(cancellationToken);

            decimal fromLastBalance = from.Balance;
            decimal transfer = request.Amount;
            decimal toLastBalance = to.Balance;
            decimal fromBalance = fromLastBalance - transfer;
            from.Balance = fromBalance;
            to.Balance = toLastBalance + transfer;
            _context.Accounts.Update(from);
            _context.Accounts.Update(to);
            await _context.SaveChangesAsync(cancellationToken);

            var dto = new TransferDto
            {
                From = from.IBAN,
                FromName = from.Name,
                To = to.IBAN,
                ToName = to.Name,
                Transfer = transfer
            };

            return dto;
        }
    }
}