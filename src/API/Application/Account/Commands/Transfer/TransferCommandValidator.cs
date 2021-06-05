using FluentValidation;

namespace API.Application.Account.Commands.Transfer
{
    public class TransferCommandValidator : AbstractValidator<TransferCommand>
    {
        public TransferCommandValidator()
        {
            RuleFor(v => v.FromIBAN)
                .NotEmpty();
            
            RuleFor(v => v.ToIBAN)
                .NotEmpty();

            RuleFor(v => v.Amount)
                .GreaterThan(0);
        }
    }
}