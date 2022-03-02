using ContaCorrente.Domain.Entities;
using ContaCorrente.Domain.Enums;
using ContaCorrente.Domain.Interfaces;
using ContaCorrente.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContaCorrente.Infra.Data.Repositories
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private ITransactionRepository _transanctionRepository;
        ApplicationDbContext _bankAccountContext;
        public BankAccountRepository(ApplicationDbContext context, ITransactionRepository transanctionRepository)
        {
            _bankAccountContext = context;
            _transanctionRepository = transanctionRepository;
        }

        public async Task<IEnumerable<BankAccount>> GetAllAccountsAsync()
        {
            return await _bankAccountContext.BankAccounts.ToListAsync();
        }

        public async Task<BankAccount> GetByAccountNumberAsync(string accountNumber)
        {
            var bankAccount = await _bankAccountContext.BankAccounts
                .Include(t => t.Transactions.Where(x => x.AccountNumber == accountNumber)).SingleOrDefaultAsync(b => b.AccountNumber == accountNumber);
            
            if (bankAccount != null)
            {
                _bankAccountContext.Entry(bankAccount).State = EntityState.Detached;
            }
            return bankAccount;
        }

        public async Task<BankAccount> GetHistoryAsync(string accountNumber)
        {
            var bankAccount = await _bankAccountContext.BankAccounts
                .Include(t => t.Transactions.Where(x => x.AccountNumber == accountNumber)).SingleOrDefaultAsync(b => b.AccountNumber == accountNumber);

            if (bankAccount != null)
            {
                _bankAccountContext.Entry(bankAccount).State = EntityState.Detached;
            }
            return bankAccount;
        }

        public async Task<BankAccount> GetPeriodHistoryAsync(string accountNumber, DateTime startDate, DateTime finalDate)
        {
            var bankAccount = await _bankAccountContext.BankAccounts
                .Include(t => t.Transactions.Where(x => x.AccountNumber == accountNumber
                    && x.Date >= startDate && x.Date <= finalDate))
                .SingleOrDefaultAsync(b => b.AccountNumber == accountNumber);

            if (bankAccount != null)
            {
                _bankAccountContext.Entry(bankAccount).State = EntityState.Detached;
            }
            return bankAccount;
        }

        public async Task<BankAccount> CreateAsync(BankAccount bankAccount)
        {
            _bankAccountContext.Add(bankAccount);
            await _bankAccountContext.SaveChangesAsync();
            
            return bankAccount;
        }

        public async Task<BankAccount> RemoveAsync(BankAccount bankAccount)
        {
            _bankAccountContext.Remove(bankAccount);
            await _bankAccountContext.SaveChangesAsync();
            return bankAccount;
        }

        public async Task<BankAccount> UpdateAsync(BankAccount bankAccount)
        {
            _bankAccountContext.Update(bankAccount);
            await _bankAccountContext.SaveChangesAsync();
            return bankAccount;
        }

        public async Task<BankAccount> DepositAsync(BankAccount bankAccount, double value, DateTime date)
        {
            try
            {
                var transaction = new Transaction(0, bankAccount.AccountNumber,
                    bankAccount.BankCode, value,
                    (int)TransactionType.Type.Deposit, date != DateTime.MinValue ? date : DateTime.Today, bankAccount.Id);

                await _transanctionRepository.CreateAsync(transaction);

                bankAccount.Deposit(value);
                
                await _bankAccountContext.SaveChangesAsync();
                await UpdateAsync(bankAccount);
            }
            catch (Exception ex)
            {
                throw new Exception("Problems to make the deposit, please try again later. " + ex);
            }

            return bankAccount;
        }

        public async Task<BankAccount> WithdrawlAsync(BankAccount bankAccount, double value, DateTime date)
        {
            try
            {
                var transaction = new Transaction(bankAccount.AccountNumber,
                    bankAccount.BankCode, value,
                    (int)TransactionType.Type.Withdrawl, date != DateTime.MinValue ? date : DateTime.Today, bankAccount.Id);

                await _bankAccountContext.Transactions.AddAsync(transaction);

                bankAccount.Withdraw(value);
                if (bankAccount.Balance < 0)
                {
                    throw new Exception("You don't have suficient funds to withdrawl");
                }

                await _bankAccountContext.SaveChangesAsync();
                await UpdateAsync(bankAccount);
            }
            catch (Exception ex)
            {
                throw new Exception("Problems to make the withdrawl, please try again later. " + ex);
            }

            return bankAccount;
        }

        public async Task<BankAccount> PaymentAsync(BankAccount bankAccount, double value, DateTime date)
        {
            try
            {
                var transaction = new Transaction(bankAccount.AccountNumber,
                    bankAccount.BankCode, value,
                    (int)TransactionType.Type.Payment, date != DateTime.MinValue ? date : DateTime.Today, bankAccount.Id);

                await _bankAccountContext.Transactions.AddAsync(transaction);

                bankAccount.Payment(value);

                if (bankAccount.Balance < 0)
                {
                    throw new Exception("You don't have suficient funds to make the payment.");
                }

                await _bankAccountContext.SaveChangesAsync();
                await UpdateAsync(bankAccount);
            }
            catch (Exception ex)
            {
                throw new Exception("Problems to make the payment, please try again later. " + ex);
            }

            return bankAccount;
        }
    }
}
