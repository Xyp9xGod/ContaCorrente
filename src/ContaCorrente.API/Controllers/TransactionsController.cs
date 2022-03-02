using ContaCorrente.Application.DTOs;
using ContaCorrente.Application.Interfaces;
using ContaCorrente.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ContaCorrente.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private ITransactionService _transactionService;
        private IBankAccountService _bankAccountService;

        public TransactionsController(ITransactionService transactionService, IBankAccountService bankAccountService)
        {
            _transactionService = transactionService;
            _bankAccountService = bankAccountService;
        }

        [HttpGet("{accountNumber}", Name = "GetAllAccountTransactions")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> Get(string accountNumber)
        {
            try
            {
                var transactions = await _transactionService.GetAllAccountTransactionsAsync(accountNumber);
                if (transactions.Count() == 0)
                {
                    return NotFound("No movimentations found.");
                }
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error getting transactions: " + ex.Message);
            }
        }

        [HttpGet("{accountNumber}/{startDate}/{finalDate}", Name = "GetTransactionsByPeriod")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> Get(string accountNumber, DateTime startDate, DateTime finalDate)
        {
            try
            {
                var transactions = await _transactionService
                    .GetTransactionsByDateAsync(accountNumber, startDate, finalDate);

                if (transactions.Count() == 0)
                {
                    return NotFound("No movimentations found on this period.");
                }
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error getting transactions: " + ex.Message);
            }
        }
                
        [HttpPost("Deposit/")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Deposit(TransactionDTO transactionDTO)
        {
            if (transactionDTO == null)
                return BadRequest("Invalid Data.");
            
            if(transactionDTO.Type != (int)TransactionType.Type.Deposit)
                return BadRequest("Invalid Type, to make this operation inform Type 1.");

            var bankAccount = await _bankAccountService.GetByAccountNumberAsync(transactionDTO.AccountNumber);
            
            if (bankAccount == null)
            {
                return NotFound("Account Not Found.");
            }

            try
            {
                await _bankAccountService.DepositAsync(bankAccount, transactionDTO.Value, transactionDTO.Date);
                return Ok(transactionDTO);
            }
            catch (Exception ex)
            {
                return BadRequest("Error trying to make the deposit: " + ex.Message);
            }
        }

        [HttpPost("Withdrawl/")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Withdrawl(TransactionDTO transactionDTO)
        {
            if (transactionDTO == null)
                return BadRequest("Invalid Data.");

            if (transactionDTO.Type != (int)TransactionType.Type.Withdrawl)
                return BadRequest("Invalid Type, to make this operation inform Type 2.");

            var bankAccount = await _bankAccountService.GetByAccountNumberAsync(transactionDTO.AccountNumber);

            if (bankAccount == null)
            {
                return NotFound("Account Not Found.");
            }

            try
            {
                await _bankAccountService.WithdrawlAsync(bankAccount, transactionDTO.Value, transactionDTO.Date);
                return Ok(transactionDTO);
            }
            catch (Exception ex)
            {
                return BadRequest("Error trying to make the withdral: " + ex.Message);
            }
        }

        [HttpPost("Payment/")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Payment(TransactionDTO transactionDTO)
        {
            if (transactionDTO == null)
                return BadRequest("Invalid Data.");

            if (transactionDTO.Type != (int)TransactionType.Type.Payment)
                return BadRequest("Invalid Type, to make this operation inform Type 3.");

            var bankAccount = await _bankAccountService.GetByAccountNumberAsync(transactionDTO.AccountNumber);

            if (bankAccount == null)
            {
                return NotFound("Account Not Found.");
            }

            try
            {
                await _bankAccountService.PaymentAsync(bankAccount, transactionDTO.Value, transactionDTO.Date);
                return Ok(transactionDTO);
            }
            catch (Exception ex)
            {
                return BadRequest("Error trying to make the payment: " + ex.Message);
            }
        }
    }
}
