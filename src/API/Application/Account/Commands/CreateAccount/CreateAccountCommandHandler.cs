using System.Threading;
using System.Threading.Tasks;
using API.Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace API.Application.Account.Commands.CreateAccount
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, CreateAccountDto>
    {
        private readonly IIBAN _iban;
        private readonly IABCBankDbContext _context;
        private readonly IMapper _mapper;

        public CreateAccountCommandHandler(IIBAN iban, IABCBankDbContext context, IMapper mapper)
        {
            _iban = iban;
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateAccountDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Account
            {
                Id = 0,
                IBAN = await _iban.Generate,
                Name = request.Name
            };
            _context.Accounts.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CreateAccountDto>(entity);
        }
    }
}