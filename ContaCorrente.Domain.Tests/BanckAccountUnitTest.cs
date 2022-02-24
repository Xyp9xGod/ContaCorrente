using ContaCorrente.Domain.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace ContaCorrente.Domain.Tests
{
    public class BanckAccountUnitTest
    {
        [Fact]
        public void CreateBankAccount_WithoutAccountNumber_ResultObjectValidState()
        {
            Action action = () => new BankAccount("", "371", 0);
            action.Should()
                .Throw<Validations.DomainExceptionValidation>()
                .WithMessage("Invalid Account Number, Account Number is required.");
        }
        
        [Fact]
        public void CreateBankAccount_WithoutBankCode_ResultObjectValidState()
        {
            Action action = () => new BankAccount("123456-0", "", 0);
            action.Should()
                .Throw<Validations.DomainExceptionValidation>()
                .WithMessage("Invalid Bank Code, Bank Code is required.");
        }
        
        [Fact]
        public void CreateBankAccount_WithoutInvalidBalance_ResultObjectValidState()
        {
            Action action = () => new BankAccount("123456-0", "371", -50.35);
            action.Should()
                .Throw<Validations.DomainExceptionValidation>()
                .WithMessage("Invalid Balance value. Value Shouldn't be negative.");
        }

        [Fact]
        public void CreateBankAccount_WithInvalidId_ResultObjectValidState()
        {
            Action action = () => new BankAccount(Guid.Empty, "123456-0", "371", 0);
            action.Should()
                .Throw<Validations.DomainExceptionValidation>()
                .WithMessage("Invalid Id value.");
        }

        [Fact]
        public void CreateBankAccount_WithValidId_ResultObjectValidState()
        {
            Action action = () => new BankAccount(Guid.NewGuid(), "123456-0", "371", 0);
            action.Should()
                .NotThrow();
        }
    }
}
