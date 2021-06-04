using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Account.Commands.DepositAccount
{
    public class DepositAccountCommandHandler : IRequestHandler<DepositAccountCommand, DepositAccountDto>
    {
        private readonly IABCBankDbContext _context;
        private readonly IMapper _mapper;

        public DepositAccountCommandHandler(IABCBankDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DepositAccountDto> Handle(DepositAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _context.Accounts
                .AsNoTracking()
                .Where(w => Equals(w.IBAN, request.IBAN))
                .SingleAsync(cancellationToken);

            decimal lastBalance = account.Balance;
            decimal fee = request.Deposit * 0.001M;
            decimal totalDeposit = request.Deposit - fee;
            decimal balance = lastBalance + totalDeposit;

            account.Balance = balance;
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<DepositAccountDto>(account);
            dto.LastBalance = lastBalance;
            dto.Deposit = request.Deposit;
            dto.Fee = fee;
            dto.TotalDeposit = totalDeposit;
            dto.Balance = balance;

            return dto;
        }
    }
}