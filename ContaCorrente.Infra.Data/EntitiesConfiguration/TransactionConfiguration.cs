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
            builder.Property(p => p.Value).HasPrecision(10,2).IsRequired();
            builder.Property(p => p.Date).IsRequired();
            builder.HasData(
                new Transaction(1, "123456-0", "371", 36.45, "C", System.DateTime.Today),
                new Transaction(2, "123456-0", "371", 11.50, "D", System.DateTime.Today),
                new Transaction(3, "345678-9", "371", 78, "C", System.DateTime.Today),
                new Transaction(4, "345678-9", "371", 96.12, "D", System.DateTime.Today)
            );
        }
    }
}
