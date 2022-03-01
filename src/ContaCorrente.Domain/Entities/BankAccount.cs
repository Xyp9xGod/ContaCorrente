using ContaCorrente.Domain.Validations;
using System;

namespace ContaCorrente.Domain.Entities
{
    public class BankAccount : Entity
    {
        public string AccountNumber { get; private set; }
        public string BankCode { get; private set; }
        public string AgencyNumber { get; private set; }
        public double Balance { get; private set; }

        public BankAccount(string accountNumber, string bankCode, string agencyNumber, double balance)
        {
            ValidateDomain(accountNumber, bankCode, agencyNumber, balance);
        }

        public BankAccount(int id, string accountNumber, string bankCode, string agencyNumber, double balance)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value.");
            Id = id;
            ValidateDomain(accountNumber, bankCode, agencyNumber, balance);
        }

        private void ValidateDomain(string accountNumber, string bankCode, string agencyNumber, double balance)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(accountNumber), "Invalid Account Number, Account Number is required.");
            DomainExceptionValidation.When(accountNumber.Length != 8, "Invalid Account Number, Account Number Should have 8 characters.");
            DomainExceptionValidation.When(string.IsNullOrEmpty(bankCode), "Invalid Bank Code, Bank Code is required.");
            DomainExceptionValidation.When(bankCode.Length != 3, "Invalid Bank Code, Bank Code Should have 3 characters.");
            DomainExceptionValidation.When(string.IsNullOrEmpty(agencyNumber), "Invalid Agency Number, Agency Number is required.");
            DomainExceptionValidation.When(agencyNumber.Length != 4, "Invalid Agency Number, Agency Number Should have 4 characters.");
            DomainExceptionValidation.When(balance < 0, "Invalid Balance value. Value Shouldn't be negative.");

            AccountNumber = accountNumber;
            BankCode = bankCode;
            AgencyNumber = agencyNumber;
            Balance = balance;
        }

        public BankAccount Deposit(double value)
        {
            Balance += value;
            return this;
        }

        public BankAccount Withdraw(double value)
        {
            Balance -= value;
            return this;
        }
        public BankAccount Payment(double value)
        {
            Balance -= value;
            return this;
        }
    }
}
