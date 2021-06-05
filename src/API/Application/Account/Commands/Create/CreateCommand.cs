using MediatR;

namespace API.Application.Account.Commands.Create
{
    public class CreateCommand : IRequest<CreateDto>
    {
        public string Name { get; set; }
    }
}