using MediatR;

namespace API.Application.Account.Queries.GetAccountByIBAN
{
    public class GetAccountByIBANQuery : IRequest<AccountByIBANDto>
    {
        public string IBAN { get; set; }
    }
}