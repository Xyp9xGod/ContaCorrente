﻿using ContaCorrente.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaCorrente.Domain.Interfaces
{
    public interface IBankAccountRepository
    {
        Task<IEnumerable<BankAccount>> GetAllAccountsAsync();
        Task<BankAccount> GetByAccountNumberAsync(string accountNumber);
        Task<BankAccount> CreateAsync(BankAccount bankAccount);
        Task<BankAccount> UpdateAsync(BankAccount bankAccount);
        Task<BankAccount> RemoveAsync(BankAccount bankAccount);
    }
}
