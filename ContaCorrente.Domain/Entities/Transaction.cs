using ContaCorrente.Domain.Validations;
using System;

namespace ContaCorrente.Domain.Entities
{
    public class Transaction : Entity
    {
        public string AccountNumber { get; private set; }
        public string BankCode { get; private set; }
        public double Value { get; private set; }
        public string Type { get; private set; }
        public DateTime Date { get; private set; }

        public Transaction(string accountNumber, string bankCode, double value, string type, DateTime date)
        {
            ValidateDomain(accountNumber, bankCode, value, type, date);
        }

        public void Update(string accountNumber, string bankCode, double value, string type, DateTime date)
        {
            ValidateDomain(accountNumber, bankCode, value, type, date);
        }

        private void ValidateDomain(string accountNumber, string bankCode, double value, string type, DateTime date)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(accountNumber), "Invalid Account Number, Account Number is required.");
            DomainExceptionValidation.When(string.IsNullOrEmpty(bankCode), "Invalid Bank Code, Bank Code is required.");
            DomainExceptionValidation.When(value < 0, "Invalid Value.");
            DomainExceptionValidation.When(string.IsNullOrEmpty(type), "Invalid Type, Type is required.");
            DomainExceptionValidation.When(!type.Trim().Equals("C") && !type.Trim().Equals("D"), "Invalid Type, Type Should be C for Credit or D for Debit");

            AccountNumber = accountNumber;
            BankCode = bankCode;
            Value = value;
            Type = type;
            Date = date;
        }

    }
}
