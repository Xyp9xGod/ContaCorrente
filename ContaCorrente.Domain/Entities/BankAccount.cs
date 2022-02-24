﻿using ContaCorrente.Domain.Validations;

namespace ContaCorrente.Domain.Entities
{
    public class BankAccount : Entity
    {
        public string AccountNumber { get; private set; }
        public string BankCode { get; private set; }
        public decimal Balance { get; private set; }

        public BankAccount(string accountNumber, string bankCode, decimal balance)
        {
            ValidateDomain(accountNumber, bankCode, balance);
        }

        public void Update(string accountNumber, string bankCode, decimal balance)
        {
            ValidateDomain(accountNumber, bankCode, balance);
        }

        private void ValidateDomain(string accountNumber, string bankCode, decimal balance)
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
