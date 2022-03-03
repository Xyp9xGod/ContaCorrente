using ContaCorrente.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaCorrente.Application.Interfaces
{
    public interface IBankAccountService
    {
        Task<IEnumerable<BankAccountModelDTO>> GetAllAccountsAsync();
        Task<BankAccountModelDTO> GetAccountAsync(string accountNumber, string bankCode, string agencyNumber);
        Task<BankAccountDTO> GetHistoryAsync(string accountNumber, string bankCode, string agencyNumber);
        Task<BankAccountDTO> GetPeriodHistoryAsync(string accountNumber, string bankCode, string agencyNumber, DateTime startDate, DateTime finalDate);
        Task Add(BankAccountModelDTO bankAccountDTO);
        Task Update(BankAccountModelDTO bankAccountDTO);
        Task Remove(BankAccountModelDTO bankAccountDTO);
        Task DepositAsync(BankAccountModelDTO bankAccountDTO, TransactionDTO transactionDTO);
        Task WithdrawlAsync(BankAccountModelDTO bankAccountDTO, TransactionDTO transactionDTO);
        Task PaymentAsync(BankAccountModelDTO bankAccountDTO, TransactionDTO transactionDTO);
    }
}
