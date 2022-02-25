using ContaCorrente.Domain.Entities;
using ContaCorrente.Domain.Interfaces;
using ContaCorrente.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContaCorrente.Infra.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        ApplicationDbContext _transactionDbContext;
        public TransactionRepository(ApplicationDbContext context)
        {
            _transactionDbContext = context;
        }

        public async Task<IEnumerable<Transaction>> GetAllAccountTransactionsAsync(string accountNumber)
        {
            var listTransactions = await _transactionDbContext.Transactions
                .Where(x => x.AccountNumber == accountNumber)
                .OrderBy(x => x.Date)
                .ToListAsync();

            IEnumerable<Transaction> transactions = listTransactions;
            
            return transactions;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByDateAsync(string accountNumber, DateTime startDate, DateTime finalDate)
        {
            var listTransactions = await _transactionDbContext.Transactions
                .Where(x => x.AccountNumber == accountNumber
                        && (x.Date >= startDate && x.Date <= finalDate))
                .OrderBy(x => x.Date)
                .ToListAsync();

            IEnumerable<Transaction> transactions = listTransactions;

            return transactions;
        }

        public async Task<Transaction> CreateAsync(Transaction transaction)
        {
            _transactionDbContext.Add(transaction);
            await _transactionDbContext.SaveChangesAsync();
            return transaction;
        }

        public async Task<Transaction> RemoveAsync(Transaction transaction)
        {
            _transactionDbContext.Remove(transaction);
            await _transactionDbContext.SaveChangesAsync();
            return transaction;
        }

        public async Task<Transaction> UpdateAsync(Transaction transaction)
        {
            _transactionDbContext.Update(transaction);
            await _transactionDbContext.SaveChangesAsync();
            return transaction;
        }
    }
}
