using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Account.Commands.Deposit
{
    public class DepositCommandHandler : IRequestHandler<DepositCommand, DepositDto>
    {
        private readonly IABCBankDbContext _context;
        private readonly IMapper _mapper;

        public DepositCommandHandler(IABCBankDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DepositDto> Handle(DepositCommand request, CancellationToken cancellationToken)
        {
            var account = await _context.Accounts
                .AsNoTracking()
                .Where(w => Equals(w.IBAN, request.IBAN))
                .SingleAsync(cancellationToken);

            decimal lastBalance = account.Balance;
            decimal fee = request.Amount * 0.001M;
            decimal netAmount = request.Amount - fee;
            decimal balance = lastBalance + netAmount;

            account.Balance = balance;
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<DepositDto>(account);
            dto.LastBalance = lastBalance;
            dto.Amount = request.Amount;
            dto.Fee = fee;
            dto.NetAmount = netAmount;
            dto.Balance = balance;

            return dto;
        }
    }
}