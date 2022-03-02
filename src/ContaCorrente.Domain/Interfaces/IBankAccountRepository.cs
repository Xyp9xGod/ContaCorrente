using ContaCorrente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaCorrente.Domain.Interfaces
{
    public interface IBankAccountRepository
    {
        Task<IEnumerable<BankAccount>> GetAllAccountsAsync();
        Task<BankAccount> GetByAccountNumberAsync(string accountNumber);
        Task<BankAccount> GetHistoryAsync(string accountNumber);
        Task<BankAccount> GetPeriodHistoryAsync(string accountNumber, DateTime startDate, DateTime finalDate);
        Task<BankAccount> CreateAsync(BankAccount bankAccount);
        Task<BankAccount> UpdateAsync(BankAccount bankAccount);
        Task<BankAccount> RemoveAsync(BankAccount bankAccount);
        Task<BankAccount> DepositAsync(BankAccount bankAccount, double value, DateTime date);
        Task<BankAccount> WithdrawlAsync(BankAccount bankAccount, double value, DateTime date);
        Task<BankAccount> PaymentAsync(BankAccount bankAccount, double value, DateTime date);
    }
}
