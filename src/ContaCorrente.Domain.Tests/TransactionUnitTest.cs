using ContaCorrente.Domain.Entities;
using ContaCorrente.Domain.Enums;
using FluentAssertions;
using System;
using Xunit;

namespace ContaCorrente.Domain.Tests
{
    public class TransactionUnitTest
    {
        [Fact]
        public void CreateTransaction_WithInvalidId_ResultObjectValidState()
        {
            Action action = () => new Transaction(-1, "123456-0", "", 50, (int)TransactionType.Type.Deposit, DateTime.Now);
            action.Should()
                .Throw<Validations.DomainExceptionValidation>()
                .WithMessage("Invalid Id value.");
        }

        [Fact]
        public void CreateTransaction_WithValidId_ResultObjectValidState()
        {
            Action action = () => new Transaction(1,"123456-0", "371", 50, (int)TransactionType.Type.Deposit, DateTime.Now);
            action.Should()
                .NotThrow();
        }

        [Fact]
        public void CreateTransaction_WithoutAccountNumber_ResultObjectValidState()
        {
            Action action = () => new Transaction("", "371", 50, (int)TransactionType.Type.Deposit, DateTime.Now);
            action.Should()
                .Throw<Validations.DomainExceptionValidation>()
                .WithMessage("Invalid Account Number, Account Number is required.");
        }

        [Fact]
        public void CreateTransaction_WithWrongAccountNumberSize_ResultObjectValidState()
        {
            Action action = () => new Transaction("00123-3", "371", 50, (int)TransactionType.Type.Deposit, DateTime.Now);
            action.Should()
                .Throw<Validations.DomainExceptionValidation>()
                .WithMessage("Invalid Account Number, Account Number Should have 8 characters.");
        }

        [Fact]
        public void CreateTransaction_WithWrongBankCodeSize_ResultObjectValidState()
        {
            Action action = () => new Transaction("123456-0", "0371", 50, (int)TransactionType.Type.Deposit, DateTime.Now);
            action.Should()
                .Throw<Validations.DomainExceptionValidation>()
                .WithMessage("Invalid Bank Code, Bank Code Should have 3 characters.");
        }

        [Fact]
        public void CreateTransaction_WithoutBankCode_ResultObjectValidState()
        {
            Action action = () => new Transaction("123456-0", "", 50, (int)TransactionType.Type.Deposit, DateTime.Now);
            action.Should()
                .Throw<Validations.DomainExceptionValidation>()
                .WithMessage("Invalid Bank Code, Bank Code is required.");
        }

        [Fact]
        public void CreateTransaction_WithInvalidType_ResultObjectValidState()
        {
            Action action = () => new Transaction("123456-0", "371", 79, 0, DateTime.Now);
            action.Should()
                .Throw<Validations.DomainExceptionValidation>()
                .WithMessage("Invalid Type.");
        }

        [Fact]
        public void CreateTransaction_WithInvalidValue_ResultObjectValidState()
        {
            Action action = () => new Transaction("123456-0", "371", -5.78, (int)TransactionType.Type.Deposit, DateTime.Now);
            action.Should()
                .Throw<Validations.DomainExceptionValidation>()
                .WithMessage("Invalid Value.");
        }
    }
}
