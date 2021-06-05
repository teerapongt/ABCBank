using API.Application.Common.Mappings;

namespace API.Application.Account.Commands.Deposit
{
    public class DepositDto : IMapFrom<Domain.Entities.Account>
    {
        public string IBAN { get; set; }
        public string Name { get; set; }
        public decimal LastBalance { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public decimal NetAmount { get; set; }
        public decimal Balance { get; set; }
    }
}