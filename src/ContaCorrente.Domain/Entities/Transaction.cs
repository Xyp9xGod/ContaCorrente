using ContaCorrente.Domain.Enums;
using ContaCorrente.Domain.Validations;
using System;

namespace ContaCorrente.Domain.Entities
{
    public class Transaction : Entity
    {
        public string AccountNumber { get; private set; }
        public string BankCode { get; private set; }
        public double Value { get; private set; }
        public int Type { get; private set; }
        public DateTime Date { get; private set; }

        public Transaction(string accountNumber, string bankCode, double value, int type, DateTime date)
        {
            ValidateDomain(accountNumber, bankCode, value, type, date);
        }

        public Transaction(int id, string accountNumber, string bankCode, double value, int type, DateTime date)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value.");
            Id = id;
            ValidateDomain(accountNumber, bankCode, value, type, date);
        }

        private void ValidateDomain(string accountNumber, string bankCode, double value, int type, DateTime date)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(accountNumber), "Invalid Account Number, Account Number is required.");
            DomainExceptionValidation.When(accountNumber.Length != 8, "Invalid Account Number, Account Number Should have 8 characters.");
            DomainExceptionValidation.When(string.IsNullOrEmpty(bankCode), "Invalid Bank Code, Bank Code is required.");
            DomainExceptionValidation.When(bankCode.Length != 3, "Invalid Bank Code, Bank Code Should have 3 characters.");
            DomainExceptionValidation.When(
                type != (int)TransactionType.Type.Deposit &&
                type != (int)TransactionType.Type.Withdrawl &&
                type != (int)TransactionType.Type.Payment
                , "Invalid Type.");
            DomainExceptionValidation.When(value < 0, "Invalid Value.");
            
            AccountNumber = accountNumber;
            BankCode = bankCode;
            Value = value;
            Type = type;
            Date = date;
        }
    }
}
