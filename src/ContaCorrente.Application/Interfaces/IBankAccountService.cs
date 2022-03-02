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
        Task<BankAccountDTO> GetHistoryAsync(string accountNumber);
        Task<BankAccountDTO> GetPeriodHistoryAsync(string accountNumber, DateTime startDate, DateTime finalDate);

        Task Add(BankAccountModelDTO bankAccountDTO);
        Task Update(BankAccountModelDTO bankAccountDTO);
        Task Remove(string accountNumber);
        Task DepositAsync(BankAccountDTO bankAccountDTO, double value, DateTime date);
        Task WithdrawlAsync(BankAccountDTO bankAccountDTO, double value, DateTime date);
        Task PaymentAsync(BankAccountDTO bankAccountDTO, double value, DateTime date);
    }
}
