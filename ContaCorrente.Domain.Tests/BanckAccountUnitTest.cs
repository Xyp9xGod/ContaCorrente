using ContaCorrente.Domain.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace ContaCorrente.Domain.Tests
{
    public class BanckAccountUnitTest
    {
        [Fact]
        public void CreateBankAccount_WithInvalidId_ResultObjectValidState()
        {
            Action action = () => new BankAccount(0, "123456-0", "371", 0);
            action.Should()
                .Throw<Validations.DomainExceptionValidation>()
                .WithMessage("Invalid Id value.");
        }

        [Fact]
        public void CreateBankAccount_WithValidId_ResultObjectValidState()
        {
            Action action = () => new BankAccount(1, "123456-0", "371", 0);
            action.Should()
                .NotThrow();
        }

        [Fact]
        public void CreateBankAccount_WithoutAccountNumber_ResultObjectValidState()
        {
            Action action = () => new BankAccount("", "371", 0);
            action.Should()
                .Throw<Validations.DomainExceptionValidation>()
                .WithMessage("Invalid Account Number, Account Number is required.");
        }

        [Fact]
        public void CreateBankAccount_WithWrongAccountNumberSize_ResultObjectValidState()
        {
            Action action = () => new BankAccount("12345-0", "371", 0);
            action.Should()
                .Throw<Validations.DomainExceptionValidation>()
                .WithMessage("Invalid Account Number, Account Number Should have 8 characters.");
        }

        [Fact]
        public void CreateBankAccount_WithWrongBankCodeSize_ResultObjectValidState()
        {
            Action action = () => new BankAccount("123456-0", "0371", 0);
            action.Should()
                .Throw<Validations.DomainExceptionValidation>()
                .WithMessage("Invalid Bank Code, Bank Code Should have 3 characters.");
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
    }
}
