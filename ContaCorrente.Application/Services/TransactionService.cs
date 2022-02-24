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
    public class TransactionService : ITransactionService
    {
        private ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        public TransactionService(IMapper mapper, ITransactionRepository transactionRepository)
        {
            _mapper = mapper;
            _transactionRepository = transactionRepository ??
                 throw new ArgumentNullException(nameof(transactionRepository));
        }

        public async Task<IEnumerable<TransactionDTO>> GetAllAccountsAsync()
        {
            var transactionEntity = await _transactionRepository.GetAllTransactionsAsync();
            return _mapper.Map<IEnumerable<TransactionDTO>>(transactionEntity);
        }

        public async Task<IEnumerable<TransactionDTO>> GetTransactionsByDateAsync(string accountNumber, DateTime startDate, DateTime finalDate)
        {
            var transactionEntity = await _transactionRepository.GetTransactionsByDateAsync(accountNumber, startDate, finalDate);
            return _mapper.Map<IEnumerable<TransactionDTO>>(transactionEntity);
        }

        public async Task Add(TransactionDTO transactionDto)
        {
            var transactionEntity = _mapper.Map<Transaction>(transactionDto);
            await _transactionRepository.CreateAsync(transactionEntity);
        }
    }
}
