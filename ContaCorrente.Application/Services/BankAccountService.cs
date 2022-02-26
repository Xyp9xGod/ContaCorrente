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

        public async Task<IEnumerable<BankAccountDTO>> GetAllAccountsAsync()
        {
            var bankAcccountEntity = await _bankAccoutRepository.GetAllAccountsAsync();
            return _mapper.Map<IEnumerable<BankAccountDTO>>(bankAcccountEntity);
        }

        public async Task<BankAccountDTO> GetByAccountNumberAsync(string accountNumber)
        {
            var bankAcccountEntity = await _bankAccoutRepository.GetByAccountNumberAsync(accountNumber);
            return _mapper.Map<BankAccountDTO>(bankAcccountEntity);
        }

        public async Task Add(BankAccountDTO bankAccountDTO)
        {
            var bankAcccountEntity = _mapper.Map<BankAccount>(bankAccountDTO);
            await _bankAccoutRepository.CreateAsync(bankAcccountEntity);
        }

        public async Task Update(BankAccountDTO bankAccountDTO)
        {
            var bankAcccountEntity = _mapper.Map<BankAccount>(bankAccountDTO);
            await _bankAccoutRepository.UpdateAsync(bankAcccountEntity);
        }

        public async Task Remove(string accountNumber)
        {
            var bankAcccountEntity = await _bankAccoutRepository.GetByAccountNumberAsync(accountNumber);
            await _bankAccoutRepository.RemoveAsync(bankAcccountEntity);
        }

        public async Task DepositAsync(BankAccountDTO bankAccountDTO, double value, DateTime date)
        {
            var bankAcccountEntity = _mapper.Map<BankAccount>(bankAccountDTO);
            await _bankAccoutRepository.DepositAsync(bankAcccountEntity, value, date);
        }
    }
}
