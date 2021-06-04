using API.Application.Common.Mappings;

namespace API.Application.Account.Commands.CreateAccount
{
    public class AccountDto : IMapFrom<Domain.Entities.Account>
    {
        public string IBAN { get; set; }
        public string Name { get; set; }
    }
}