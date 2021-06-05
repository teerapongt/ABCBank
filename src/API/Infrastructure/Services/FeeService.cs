using API.Application.Common.Interfaces;

namespace API.Infrastructure.Services
{
    public class FeeService : IFee
    {
        public decimal Deposit => 0.001M;
    }
}