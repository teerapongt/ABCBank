using MediatR;

namespace API.Application.Account.Commands.CreateAccount
{
    public class CreateAccountCommand : IRequest<string>
    {
        public string Name { get; set; }
    }
}