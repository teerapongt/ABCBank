using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Account.Queries.GetAccountByIBAN
{
    public class GetAccountByIBANQueryHandler : IRequestHandler<GetAccountByIBANQuery, AccountByIBANDto>
    {
        private readonly IABCBankDbContext _context;
        private readonly IMapper _mapper;

        public GetAccountByIBANQueryHandler(IABCBankDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AccountByIBANDto> Handle(GetAccountByIBANQuery request, CancellationToken cancellationToken)
        {
            var dto = await _context.Accounts
                .AsNoTracking()
                .Where(w => Equals(w.IBAN, request.IBAN))
                .ProjectTo<AccountByIBANDto>(_mapper.ConfigurationProvider)
                .SingleAsync(cancellationToken);
            
            return dto;
        }
    }
}