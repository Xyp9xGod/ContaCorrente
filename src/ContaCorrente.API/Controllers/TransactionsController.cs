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
        private IBankAccountService _bankAccountService;

        public TransactionsController(IBankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
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

            var bankAccount = await _bankAccountService.GetAccountAsync(transactionDTO.AccountNumber, transactionDTO.BankCode, transactionDTO.AgencyNumber);
            
            if (bankAccount == null)
            {
                return NotFound("Account Not Found.");
            }

            try
            {
                await _bankAccountService.DepositAsync(bankAccount, transactionDTO);
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

            var bankAccount = await _bankAccountService.GetAccountAsync(transactionDTO.AccountNumber, transactionDTO.BankCode, transactionDTO.AgencyNumber);

            if (bankAccount == null)
            {
                return NotFound("Account Not Found.");
            }

            try
            {
                await _bankAccountService.WithdrawlAsync(bankAccount, transactionDTO);
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

            var bankAccount = await _bankAccountService.GetAccountAsync(transactionDTO.AccountNumber, transactionDTO.BankCode, transactionDTO.AgencyNumber);

            if (bankAccount == null)
            {
                return NotFound("Account Not Found.");
            }

            try
            {
                await _bankAccountService.PaymentAsync(bankAccount, transactionDTO);
                return Ok(transactionDTO);
            }
            catch (Exception ex)
            {
                return BadRequest("Error trying to make the payment: " + ex.Message);
            }
        }
    }
}
