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
            builder.Property(p => p.AgencyNumber).HasMaxLength(4).IsRequired();
            builder.Property(p => p.Value).IsRequired();
            builder.Property(p => p.Date).IsRequired();
            builder.HasOne(p => p.BankAccount).WithMany(p => p.Transactions)
                .HasForeignKey(p => p.BankAccountId);
        }
    }
}
