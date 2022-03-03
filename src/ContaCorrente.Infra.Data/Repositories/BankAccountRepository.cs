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

        public async Task<BankAccount> GetAccountAsync(string accountNumber, string bankCode, string agencyNumber)
        {
            var bankAccount = await _bankAccountContext.BankAccounts
                .Include(t => t.Transactions.Where(
                    x => x.AccountNumber == accountNumber &&
                    x.BankCode == bankCode &&
                    x.AgencyNumber == agencyNumber))
                .SingleOrDefaultAsync(b => b.AccountNumber == accountNumber &&
                    b.BankCode == bankCode &&
                    b.AgencyNumber == agencyNumber);

            if (bankAccount != null)
            {
                _bankAccountContext.Entry(bankAccount).State = EntityState.Detached;
            }
            return bankAccount;
        }

        public async Task<BankAccount> GetHistoryAsync(string accountNumber, string bankCode, string agencyNumber)
        {
            var bankAccount = await _bankAccountContext.BankAccounts
                .Include(t => t.Transactions.Where(
                    x => x.AccountNumber == accountNumber && 
                    x.BankCode == bankCode && 
                    x.AgencyNumber == agencyNumber))
                .SingleOrDefaultAsync(b => b.AccountNumber == accountNumber &&
                    b.BankCode == bankCode && 
                    b.AgencyNumber == agencyNumber);

            if (bankAccount != null)
            {
                _bankAccountContext.Entry(bankAccount).State = EntityState.Detached;
            }
            return bankAccount;
        }

        public async Task<BankAccount> GetPeriodHistoryAsync(string accountNumber, string bankCode, 
            string agencyNumber, DateTime startDate, DateTime finalDate)
        {
            var bankAccount = await _bankAccountContext.BankAccounts
                .Include(t => t.Transactions.Where(
                    x => x.AccountNumber == accountNumber &&
                    x.BankCode == bankCode &&
                    x.AgencyNumber == agencyNumber &&
                    x.Date >= startDate && x.Date <= finalDate))
                .SingleOrDefaultAsync(b => b.AccountNumber == accountNumber &&
                    b.BankCode == bankCode &&
                    b.AgencyNumber == agencyNumber);

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

        public async Task<BankAccount> DepositAsync(BankAccount bankAccount, Transaction transaction)
        {
            try
            {
                var newTransaction = new Transaction(0, bankAccount.AccountNumber,
                    bankAccount.BankCode, bankAccount.AgencyNumber, transaction.Value,
                    transaction.Type,
                    transaction.Date != DateTime.MinValue ? transaction.Date : DateTime.Today, bankAccount.Id);

                await _transanctionRepository.CreateAsync(newTransaction);

                bankAccount.Deposit(transaction.Value);
                
                await _bankAccountContext.SaveChangesAsync();
                await UpdateAsync(bankAccount);
            }
            catch (Exception ex)
            {
                throw new Exception("Problems to make the deposit, please try again later. " + ex);
            }

            return bankAccount;
        }

        public async Task<BankAccount> WithdrawlAsync(BankAccount bankAccount, Transaction transaction)
        {
            try
            {
                var newTransaction = new Transaction(0, bankAccount.AccountNumber,
                    bankAccount.BankCode, bankAccount.AgencyNumber, transaction.Value,
                    transaction.Type,
                    transaction.Date != DateTime.MinValue ? transaction.Date : DateTime.Today, bankAccount.Id);

                await _transanctionRepository.CreateAsync(newTransaction);

                bankAccount.Withdraw(transaction.Value);

                if (bankAccount.Balance < 0)                
                    throw new Exception("You don't have suficient funds to withdrawl");
                
                await _bankAccountContext.SaveChangesAsync();
                await UpdateAsync(bankAccount);
            }
            catch (Exception ex)
            {
                throw new Exception("Problems to make the withdrawl, please try again later. " + ex);
            }

            return bankAccount;
        }

        public async Task<BankAccount> PaymentAsync(BankAccount bankAccount, Transaction transaction)
        {
            try
            {
                var newTransaction = new Transaction(0, bankAccount.AccountNumber,
                    bankAccount.BankCode, bankAccount.AgencyNumber, transaction.Value,
                    transaction.Type,
                    transaction.Date != DateTime.MinValue ? transaction.Date : DateTime.Today, bankAccount.Id);

                await _transanctionRepository.CreateAsync(newTransaction);

                bankAccount.Payment(transaction.Value);

                if (bankAccount.Balance < 0)
                    throw new Exception("You don't have suficient funds to make the payment.");

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
