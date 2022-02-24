using ContaCorrente.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaCorrente.Domain.Interfaces
{
    public interface IBankAccountRepository
    {
        Task<IEnumerable<BankAccount>> GetAccountsAsync();
        Task<BankAccount> GetByAccountNumberAsync(string accountNumber);
        Task<BankAccount> UpdateAsync(BankAccount bankAccount);
    }
}
