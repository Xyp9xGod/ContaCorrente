using ContaCorrente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaCorrente.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetTransactionsAsync();
        Task<IEnumerable<Transaction>> GetByDateAsync(DateTime startDate, DateTime finalDate);
    }
}
