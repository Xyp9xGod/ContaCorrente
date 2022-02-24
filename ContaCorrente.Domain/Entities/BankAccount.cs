using ContaCorrente.Domain.Validations;
using System;

namespace ContaCorrente.Domain.Entities
{
    public class BankAccount : Entity
    {
        public string AccountNumber { get; private set; }
        public string BankCode { get; private set; }
        public double Balance { get; private set; }

        public BankAccount(string accountNumber, string bankCode, double balance)
        {
            ValidateDomain(accountNumber, bankCode, balance);
        }

        public BankAccount(Guid id, string accountNumber, string bankCode, double balance)
        {
            DomainExceptionValidation.When(id.ToString().Trim() == "00000000-0000-0000-0000-000000000000", "Invalid Id value.");
            Id = id;
            ValidateDomain(accountNumber, bankCode, balance);
        }

        public void Update(string accountNumber, string bankCode, double balance)
        {
            ValidateDomain(accountNumber, bankCode, balance);
        }

        private void ValidateDomain(string accountNumber, string bankCode, double balance)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(accountNumber), "Invalid Account Number, Account Number is required.");
            DomainExceptionValidation.When(string.IsNullOrEmpty(bankCode), "Invalid Bank Code, Bank Code is required.");
            DomainExceptionValidation.When(balance < 0, "Invalid Balance value. Value Shouldn't be negative.");

            AccountNumber = accountNumber;
            BankCode = bankCode;
            Balance = balance;
        }
    }
}
