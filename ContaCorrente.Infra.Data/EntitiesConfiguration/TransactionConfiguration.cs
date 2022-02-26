using ContaCorrente.Domain.Entities;
using ContaCorrente.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContaCorrente.Infra.Data.EntitiesConfiguration
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.AccountNumber).HasMaxLength(8).IsRequired();
            builder.Property(p => p.BankCode).HasMaxLength(3).IsRequired();
            builder.Property(p => p.Value).IsRequired();
            builder.Property(p => p.Date).IsRequired();
            builder.HasData(
                new Transaction(1, "123456-0", "371", 36.45, (int)TransactionType.Type.Deposit, System.DateTime.Today),
                new Transaction(2, "123456-0", "371", 11.50, (int)TransactionType.Type.Withdrawl, System.DateTime.Today),
                new Transaction(3, "345678-9", "371", 78, (int)TransactionType.Type.Deposit, System.DateTime.Today),
                new Transaction(4, "345678-9", "371", 96.12, (int)TransactionType.Type.Payment, System.DateTime.Today)
            );
        }
    }
}
