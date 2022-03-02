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

        public async Task<BankAccountModelDTO> GetByAccountNumberAsync(string accountNumber)
        {
            var bankAcccountEntity = await _bankAccoutRepository.GetByAccountNumberAsync(accountNumber);
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

        public async Task Remove(string accountNumber)
        {
            var bankAcccountEntity = await _bankAccoutRepository.GetByAccountNumberAsync(accountNumber);
            await _bankAccoutRepository.RemoveAsync(bankAcccountEntity);
        }

        public async Task DepositAsync(BankAccountModelDTO bankAccountDTO, double value, DateTime date)
        {
            var bankAcccountEntity = _mapper.Map<BankAccount>(bankAccountDTO);
            await _bankAccoutRepository.DepositAsync(bankAcccountEntity, value, date);
        }

        public async Task WithdrawlAsync(BankAccountModelDTO bankAccountDTO, double value, DateTime date)
        {
            var bankAcccountEntity = _mapper.Map<BankAccount>(bankAccountDTO);
            await _bankAccoutRepository.WithdrawlAsync(bankAcccountEntity, value, date);
        }

        public async Task PaymentAsync(BankAccountModelDTO bankAccountDTO, double value, DateTime date)
        {
            var bankAcccountEntity = _mapper.Map<BankAccount>(bankAccountDTO);
            await _bankAccoutRepository.PaymentAsync(bankAcccountEntity, value, date);
        }

        public async Task<BankAccountDTO> GetHistoryAsync(string accountNumber)
        {
            var bankAcccountEntity = await _bankAccoutRepository.GetHistoryAsync(accountNumber);
            return _mapper.Map<BankAccountDTO>(bankAcccountEntity);
        }

        public async Task<BankAccountDTO> GetPeriodHistoryAsync(string accountNumber, DateTime startDate, DateTime finalDate)
        {
            var bankAcccountEntity = await _bankAccoutRepository.GetPeriodHistoryAsync(accountNumber, startDate, finalDate);
            return _mapper.Map<BankAccountDTO>(bankAcccountEntity);
        }
    }
}
