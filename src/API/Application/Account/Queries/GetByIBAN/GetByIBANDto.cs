using API.Application.Common.Mappings;

namespace API.Application.Account.Queries.GetByIBAN
{
    public class GetByIBANDto : IMapFrom<Domain.Entities.Account>
    {
        public string IBAN { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
    }
}