using ContaCorrente.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ContaCorrente.Infra.Data.EntitiesConfiguration
{
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.AccountNumber).HasMaxLength(100).IsRequired();
            builder.Property(p => p.BankCode).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Balance).HasMaxLength(11).IsRequired();
            builder.HasData(
                new BankAccount(Guid.NewGuid(), "123456-0", "371", 37),
                new BankAccount(Guid.NewGuid(), "678910-2", "371", 79),
                new BankAccount(Guid.NewGuid(), "345678-9", "371", 135)
            );
        }
    }
}
