using ContaCorrente.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaCorrente.Application.Interfaces
{
    public interface IBankAccountService
    {
        Task<IEnumerable<BankAccountDTO>> GetAllAccountsAsync();
        Task<BankAccountDTO> GetByAccountNumberAsync(string accountNumber);
        Task Add(BankAccountDTO bankAccountDTO);
        Task Update(BankAccountDTO bankAccountDTO);
        Task Remove(string accountNumber);
        Task DepositAsync(BankAccountDTO bankAccountDTO, double value, DateTime date);
        Task WithdrawlAsync(BankAccountDTO bankAccountDTO, double value, DateTime date);
        Task PaymentAsync(BankAccountDTO bankAccountDTO, double value, DateTime date);
    }
}
