using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Common.Interfaces
{
    public interface IABCBankDbContext
    {
        DbSet<Domain.Entities.Account> Accounts { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}