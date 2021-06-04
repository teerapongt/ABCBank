using MediatR;

namespace API.Application.Account.Commands.CreateAccount
{
    public class CreateAccountCommand : IRequest<CreateAccountDto>
    {
        public string Name { get; set; }
    }
}