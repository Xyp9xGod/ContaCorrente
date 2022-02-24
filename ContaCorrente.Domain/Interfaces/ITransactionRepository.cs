using ContaCorrente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaCorrente.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
        Task<IEnumerable<Transaction>> GetTransactionsByDateAsync(string accountNumber, DateTime startDate, DateTime finalDate);
        Task<Transaction> CreateAsync(Transaction transaction);
        Task<Transaction> UpdateAsync(Transaction transaction);
        Task<Transaction> RemoveAsync(Transaction transaction);
    }
}
