using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using API.Application.Common.Interfaces;
using API.Domain.Entities;
using API.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Persistence
{
    public class ABCBankDbContext : DbContext, IABCBankDbContext
    {
        private readonly ICurrentUser _currentUser;
        private readonly IDateTime _dateTime;

        public ABCBankDbContext(
            DbContextOptions<ABCBankDbContext> options,
            ICurrentUser currentUser,
            IDateTime dateTime
        ) : base(options)
        {
            _currentUser = currentUser;
            _dateTime = dateTime;
        }

        public DbSet<Account> Accounts { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker
                .Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUser.Username;
                        entry.Entity.Created = _dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUser.Username;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}