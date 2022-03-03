using AutoMapper;
using ContaCorrente.Application.DTOs;
using ContaCorrente.Application.Interfaces;
using ContaCorrente.Domain.Entities;
using ContaCorrente.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaCorrente.Application.Services
{
    public class BankAccountService : IBankAccountService
    {
        private IBankAccountRepository _bankAccoutRepository;
        private readonly IMapper _mapper;

        public BankAccountService(IMapper mapper, IBankAccountRepository bankAccountRepository)
        {
            _mapper = mapper;
            _bankAccoutRepository = bankAccountRepository ??
                 throw new ArgumentNullException(nameof(bankAccountRepository));
        }

        public async Task<IEnumerable<BankAccountModelDTO>> GetAllAccountsAsync()
        {
            var bankAcccountEntity = await _bankAccoutRepository.GetAllAccountsAsync();
            return _mapper.Map<IEnumerable<BankAccountModelDTO>>(bankAcccountEntity);
        }

        public async Task<BankAccountModelDTO> GetAccountAsync(string accountNumber, string bankCode, string agencyNumber)
        {
            var bankAcccountEntity = await _bankAccoutRepository.GetAccountAsync(accountNumber, bankCode, agencyNumber);
            return _mapper.Map<BankAccountModelDTO>(bankAcccountEntity);
        }

        public async Task Add(BankAccountModelDTO bankAccountDTO)
        {
            var bankAcccountEntity = _mapper.Map<BankAccount>(bankAccountDTO);
            await _bankAccoutRepository.CreateAsync(bankAcccountEntity);
        }

        public async Task Update(BankAccountModelDTO bankAccountDTO)
        {
            var bankAcccountEntity = _mapper.Map<BankAccount>(bankAccountDTO);
            await _bankAccoutRepository.UpdateAsync(bankAcccountEntity);
        }

        public async Task Remove(BankAccountModelDTO bankAccountDTO)
        {
            var bankAcccountEntity = _mapper.Map<BankAccount>(bankAccountDTO);
            await _bankAccoutRepository.RemoveAsync(bankAcccountEntity);
        }

        public async Task DepositAsync(BankAccountModelDTO bankAccountDTO, TransactionDTO transactionDTO)
        {
            var bankAcccountEntity = _mapper.Map<BankAccount>(bankAccountDTO);
            var transactionEntity = _mapper.Map<Transaction>(transactionDTO);
            await _bankAccoutRepository.DepositAsync(bankAcccountEntity, transactionEntity);
        }

        public async Task WithdrawlAsync(BankAccountModelDTO bankAccountDTO, TransactionDTO transactionDTO)
        {
            var bankAcccountEntity = _mapper.Map<BankAccount>(bankAccountDTO);
            var transactionEntity = _mapper.Map<Transaction>(transactionDTO);
            await _bankAccoutRepository.WithdrawlAsync(bankAcccountEntity, transactionEntity);
        }

        public async Task PaymentAsync(BankAccountModelDTO bankAccountDTO, TransactionDTO transactionDTO)
        {
            var bankAcccountEntity = _mapper.Map<BankAccount>(bankAccountDTO);
            var transactionEntity = _mapper.Map<Transaction>(transactionDTO);
            await _bankAccoutRepository.PaymentAsync(bankAcccountEntity, transactionEntity);
        }

        public async Task<BankAccountDTO> GetHistoryAsync(string accountNumber, string bankCode, string agencyNumber)
        {
            var bankAcccountEntity = await _bankAccoutRepository.GetHistoryAsync(accountNumber, bankCode, agencyNumber);
            return _mapper.Map<BankAccountDTO>(bankAcccountEntity);
        }

        public async Task<BankAccountDTO> GetPeriodHistoryAsync(string accountNumber, string bankCode, string agencyNumber,
            DateTime startDate, DateTime finalDate)
        {
            var bankAcccountEntity = await _bankAccoutRepository.GetPeriodHistoryAsync(accountNumber, bankCode, agencyNumber, startDate, finalDate);
            return _mapper.Map<BankAccountDTO>(bankAcccountEntity);
        }
    }
}
