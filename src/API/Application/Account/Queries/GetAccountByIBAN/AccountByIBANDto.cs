using API.Application.Common.Mappings;

namespace API.Application.Account.Queries.GetAccountByIBAN
{
    public class AccountByIBANDto : IMapFrom<Domain.Entities.Account>
    {
        public string IBAN { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
    }
}