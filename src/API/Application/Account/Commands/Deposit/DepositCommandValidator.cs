using FluentValidation;

namespace API.Application.Account.Commands.Deposit
{
    public class DepositCommandValidator : AbstractValidator<DepositCommand>
    {
        public DepositCommandValidator()
        {
            RuleFor(v => v.IBAN)
                .NotEmpty();

            RuleFor(v => v.Amount)
                .GreaterThan(0);
        }
    }
}