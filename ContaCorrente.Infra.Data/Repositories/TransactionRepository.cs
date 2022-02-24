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

        public async Task<IEnumerable<Transaction>> GetTransactionsByDateAsync(string accountNumber, DateTime startDate, DateTime finalDate)
        {
            var transactions = _transactionDbContext.Transactions.Where(x => x.AccountNumber == accountNumber 
                                && (x.Date >= startDate && x.Date <= finalDate));

            if (transactions != null)
            {
                _transactionDbContext.Entry(transactions).State = EntityState.Detached;
            }
            return await (Task<IEnumerable<Transaction>>)transactions;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _transactionDbContext.Transactions.ToListAsync();
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
