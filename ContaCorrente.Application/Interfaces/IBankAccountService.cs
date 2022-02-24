using ContaCorrente.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaCorrente.Application.Interfaces
{
    public interface IBankAccountService
    {
        Task<IEnumerable<BankAccountDTO>> GetAllAccountsAsync();
        Task<BankAccountDTO> GetByAccountNumberAsync(string accountNumber);
        Task Add(BankAccountDTO bankAccountDto);
        Task Update(BankAccountDTO bankAccountDto);
        Task Remove(string accountNumber);
    }
}
