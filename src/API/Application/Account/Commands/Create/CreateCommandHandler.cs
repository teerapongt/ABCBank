using System.Threading;
using System.Threading.Tasks;
using API.Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace API.Application.Account.Commands.Create
{
    public class CreateCommandHandler : IRequestHandler<CreateCommand, CreateDto>
    {
        private readonly IIBAN _iban;
        private readonly IABCBankDbContext _context;
        private readonly IMapper _mapper;

        public CreateCommandHandler(IIBAN iban, IABCBankDbContext context, IMapper mapper)
        {
            _iban = iban;
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateDto> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Account
            {
                Id = 0,
                IBAN = await _iban.Generate,
                Name = request.Name
            };
            _context.Accounts.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CreateDto>(entity);
        }
    }
}