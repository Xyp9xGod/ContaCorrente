using ContaCorrente.Domain.Entities;
using ContaCorrente.Domain.Enums;
using ContaCorrente.Domain.Interfaces;
using ContaCorrente.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaCorrente.Infra.Data.Repositories
{
    public class BankAccountRepository : IBankAccountRepository
    {
        ApplicationDbContext _bankAccountContext;
        public BankAccountRepository(ApplicationDbContext context)
        {
            _bankAccountContext = context;
        }

        public async Task<IEnumerable<BankAccount>> GetAllAccountsAsync()
        {
            return await _bankAccountContext.BankAccounts.ToListAsync();
        }

        public async Task<BankAccount> GetByAccountNumberAsync(string accountNumber)
        {
            var bankAccount = await _bankAccountContext.Set<BankAccount>().FirstOrDefaultAsync(x => x.AccountNumber == accountNumber);
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
                var transaction = new Transaction(bankAccount.AccountNumber,
                    bankAccount.BankCode, value,
                    (int)TransactionType.Type.Deposit, date != DateTime.MinValue ? date : DateTime.Today);
                
                await _bankAccountContext.Transactions.AddAsync(transaction);
                
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
                    (int)TransactionType.Type.Withdrawl, date != DateTime.MinValue ? date : DateTime.Today);
                
                await _bankAccountContext.Transactions.AddAsync(transaction);

                bankAccount.Withdraw(value);

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
                    (int)TransactionType.Type.Payment, date != DateTime.MinValue ? date : DateTime.Today);

                await _bankAccountContext.Transactions.AddAsync(transaction);

                bankAccount.Payment(value);

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
