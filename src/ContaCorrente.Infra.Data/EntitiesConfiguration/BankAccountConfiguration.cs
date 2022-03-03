using ContaCorrente.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContaCorrente.Infra.Data.EntitiesConfiguration
{
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.AccountNumber).HasMaxLength(8).IsRequired();
            builder.Property(p => p.BankCode).HasMaxLength(3).IsRequired();
            builder.Property(p => p.AgencyNumber).HasMaxLength(4).IsRequired();
            builder.Property(p => p.Balance).IsRequired();
            builder.HasData(
                new BankAccount(1, "123456-0", "371", "0001", 40),
                new BankAccount(2, "678910-2", "371", "0001", 60),
                new BankAccount(3, "345678-9", "371", "0001", 150)
            );
        }
    }
}
