using API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Infrastructure.Persistence.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasAlternateKey(a => a.IBAN);
            
            builder.Property(p => p.Id)
                .UseIdentityColumn()
                .IsRequired();
            
            builder.Property(p => p.IBAN)
                .HasMaxLength(18)
                .IsRequired();
            
            builder.Property(p => p.Name)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(p => p.Balance)
                .HasPrecision(19, 4)
                .HasDefaultValue(0)
                .IsRequired();
        }
    }
}