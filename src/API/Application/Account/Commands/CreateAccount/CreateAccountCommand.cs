using MediatR;

namespace API.Application.Account.Commands.CreateAccount
{
    public class CreateAccountCommand : IRequest<AccountDto>
    {
        public string Name { get; set; }
    }
}