using MediatR;

namespace API.Application.Account.Queries.GetByIBAN
{
    public class GetByIBANQuery : IRequest<GetByIBANDto>
    {
        public string IBAN { get; set; }
    }
}