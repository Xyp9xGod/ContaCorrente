using ContaCorrente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaCorrente.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllAccountTransactionsAsync(string accountNumber);
        Task<IEnumerable<Transaction>> GetTransactionsByDateAsync(string accountNumber, DateTime startDate, DateTime finalDate);
        Task<Transaction> CreateAsync(Transaction transaction);
    }
}
