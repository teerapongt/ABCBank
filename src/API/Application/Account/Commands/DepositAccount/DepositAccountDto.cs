using API.Application.Common.Mappings;
using AutoMapper;

namespace API.Application.Account.Commands.DepositAccount
{
    public class DepositAccountDto : IMapFrom<Domain.Entities.Account>
    {
        public string IBAN { get; set; }
        public string Name { get; set; }
        public decimal LastBalance { get; set; }
        public decimal Deposit { get; set; }
        public decimal Fee { get; set; }
        public decimal TotalDeposit { get; set; }
        public decimal Balance { get; set; }
    }
}