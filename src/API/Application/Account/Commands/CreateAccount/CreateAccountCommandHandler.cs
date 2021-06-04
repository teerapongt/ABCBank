using System.Threading;
using System.Threading.Tasks;
using API.Application.Common.Interfaces;
using MediatR;

namespace API.Application.Account.Commands.CreateAccount
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, string>
    {
        private readonly IIBAN _iban;
        
        public CreateAccountCommandHandler(IIBAN iban)
        {
            _iban = iban;
        }

        public async Task<string> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var iban = await _iban.Generate;
            return await Task.Run(() => $"return from create account command(Name: {request.Name}, IBAN: {iban})", cancellationToken);
        }
    }
}