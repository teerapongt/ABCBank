using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace API.Application.Account.Commands.CreateAccount
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, string>
    {
        public async Task<string> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            return await Task.Run(() => $"return from create account command(Name: {request.Name})", cancellationToken);
        }
    }
}