using API.Application.Common.Mappings;

namespace API.Application.Account.Commands.Create
{
    public class CreateDto : IMapFrom<Domain.Entities.Account>
    {
        public string IBAN { get; set; }
        public string Name { get; set; }
    }
}