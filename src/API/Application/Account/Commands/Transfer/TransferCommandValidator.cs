using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Account.Commands.Transfer
{
    public class TransferCommandValidator : AbstractValidator<TransferCommand>
    {
        private readonly IABCBankDbContext _context;

        public TransferCommandValidator(IABCBankDbContext context)
        {
            _context = context;

            RuleFor(v => v.FromIBAN)
                .NotEmpty();

            RuleFor(v => v.ToIBAN)
                .NotEmpty();
            
            RuleFor(x => x)
                .MustAsync(BeTransfer).WithMessage("Your account has insufficient funds for this transaction.");
        }

        private async Task<bool> BeTransfer(TransferCommand command, CancellationToken cancellationToken)
        {
            return await _context.Accounts
                .AnyAsync(w => Equals(w.IBAN, command.FromIBAN)
                               && w.Balance > command.Amount
                               && command.Amount > 0,
                    cancellationToken);
        }
    }
}