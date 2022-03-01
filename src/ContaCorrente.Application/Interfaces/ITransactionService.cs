using ContaCorrente.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaCorrente.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDTO>> GetAllAccountTransactionsAsync(string accountNumber);
        Task<IEnumerable<TransactionDTO>> GetTransactionsByDateAsync(string accountNumber, DateTime startDate, DateTime finalDate);
        Task Add(TransactionDTO transactionDto);
    }
}
