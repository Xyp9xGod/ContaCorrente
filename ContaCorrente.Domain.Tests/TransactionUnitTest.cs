using ContaCorrente.Domain.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace ContaCorrente.Domain.Tests
{
    public class TransactionUnitTest
    {
        [Fact]
        public void CreateTransaction_WithoutAccountNumber_ResultObjectValidState()
        {
            Action action = () => new Transaction("", "371", 50, "C", DateTime.Now);
            action.Should()
                .Throw<Validations.DomainExceptionValidation>()
                .WithMessage("Invalid Account Number, Account Number is required.");
        }

        [Fact]
        public void CreateTransaction_WithoutBankCode_ResultObjectValidState()
        {
            Action action = () => new Transaction("123456-0", "", 50, "C", DateTime.Now);
            action.Should()
                .Throw<Validations.DomainExceptionValidation>()
                .WithMessage("Invalid Bank Code, Bank Code is required.");
        }

        [Fact]
        public void CreateTransaction_WithInvalidValue_ResultObjectValidState()
        {
            Action action = () => new Transaction("123456-0", "371", -5.78, "C", DateTime.Now);
            action.Should()
                .Throw<Validations.DomainExceptionValidation>()
                .WithMessage("Invalid Value.");
        }

        [Fact]
        public void CreateTransaction_WithoutType_ResultObjectValidState()
        {
            Action action = () => new Transaction("123456-0", "371", 50.00, "", DateTime.Now);
            action.Should()
                .Throw<Validations.DomainExceptionValidation>()
                .WithMessage("Invalid Type, Type is required.");
        }

        [Fact]
        public void CreateTransaction_WithInvalideType_ResultObjectValidState()
        {
            Action action = () => new Transaction("123456-0", "371", 89.35, "F", DateTime.Now);
            action.Should()
                .Throw<Validations.DomainExceptionValidation>()
                .WithMessage("Invalid Type, Type Should be C for Credit or D for Debit");
        }

        [Fact]
        public void CreateTransaction_WithValideType_ResultObjectValidState()
        {
            Action action = () => new Transaction("123456-0", "371", 78.96, "C", DateTime.Now);
            action.Should()
                .NotThrow();
        }

        [Fact]
        public void CreateTransaction_WithInvalidId_ResultObjectValidState()
        {
            Action action = () => new BankAccount(Guid.Empty, "123456-0", "371", 0);
            action.Should()
                .Throw<Validations.DomainExceptionValidation>()
                .WithMessage("Invalid Id value.");
        }

        [Fact]
        public void CreateTransaction_WithValidId_ResultObjectValidState()
        {
            Action action = () => new BankAccount(Guid.NewGuid(), "123456-0", "371", 0);
            action.Should()
                .NotThrow();
        }
    }
}
