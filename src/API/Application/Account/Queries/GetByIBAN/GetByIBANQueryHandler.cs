using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Account.Queries.GetByIBAN
{
    public class GetByIBANQueryHandler : IRequestHandler<GetByIBANQuery, GetByIBANDto>
    {
        private readonly IABCBankDbContext _context;
        private readonly IMapper _mapper;

        public GetByIBANQueryHandler(IABCBankDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetByIBANDto> Handle(GetByIBANQuery request, CancellationToken cancellationToken)
        {
            var dto = await _context.Accounts
                .AsNoTracking()
                .Where(w => Equals(w.IBAN, request.IBAN))
                .ProjectTo<GetByIBANDto>(_mapper.ConfigurationProvider)
                .SingleAsync(cancellationToken);
            
            return dto;
        }
    }
}