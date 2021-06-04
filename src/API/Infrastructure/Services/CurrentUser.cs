using API.Application.Common.Interfaces;

namespace API.Infrastructure.Services
{
    public class CurrentUser : ICurrentUser
    {
        public string Username => "ABCBank";
    }
}