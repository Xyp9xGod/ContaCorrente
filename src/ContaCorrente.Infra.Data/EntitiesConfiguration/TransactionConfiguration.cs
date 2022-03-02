using ContaCorrente.Domain.Entities;
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
            builder.HasOne(p => p.BankAccount).WithMany(p => p.Transactions)
                .HasForeignKey(p => p.BankAccountId);
            //builder.HasData(
            //    new Transaction(1, "123456-0", "371", 50, (int)TransactionType.Type.Deposit, System.DateTime.Today),
            //    new Transaction(2, "123456-0", "371", 10, (int)TransactionType.Type.Withdrawl, System.DateTime.Today),
            //    new Transaction(3, "678910-2", "371", 70, (int)TransactionType.Type.Deposit, System.DateTime.Today),
            //    new Transaction(4, "678910-2", "371", 10, (int)TransactionType.Type.Payment, System.DateTime.Today),
            //    new Transaction(5, "345678-9", "371", 90, (int)TransactionType.Type.Deposit, System.DateTime.Today),
            //    new Transaction(6, "345678-9", "371", 60, (int)TransactionType.Type.Deposit, System.DateTime.Today)
            //);
        }
    }
}
