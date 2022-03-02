using ContaCorrente.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaCorrente.Application.Interfaces
{
    public interface ITransactionService
    {
        Task Add(TransactionDTO transactionDto);
    }
}
