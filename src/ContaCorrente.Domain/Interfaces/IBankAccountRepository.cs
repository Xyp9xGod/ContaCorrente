using ContaCorrente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaCorrente.Domain.Interfaces
{
    public interface IBankAccountRepository
    {
        Task<IEnumerable<BankAccount>> GetAllAccountsAsync();
        Task<BankAccount> GetAccountAsync(string accountNumber, string bankCode, string agencyNumber);
        Task<BankAccount> GetHistoryAsync(string accountNumber, string bankCode, string agencyNumber);
        Task<BankAccount> GetPeriodHistoryAsync(string accountNumber, string bankCode, string agencyNumber, DateTime startDate, DateTime finalDate);
        Task<BankAccount> CreateAsync(BankAccount bankAccount);
        Task<BankAccount> UpdateAsync(BankAccount bankAccount);
        Task<BankAccount> RemoveAsync(BankAccount bankAccount);
        Task<BankAccount> DepositAsync(BankAccount bankAccount, Transaction transaction);
        Task<BankAccount> WithdrawlAsync(BankAccount bankAccount, Transaction transaction);
        Task<BankAccount> PaymentAsync(BankAccount bankAccount, Transaction transaction);
    }
}
