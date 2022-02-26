using ContaCorrente.Application.DTOs;
using ContaCorrente.Application.Interfaces;
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

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
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
                    return NotFound("No movimentations found on this periodo.");
                }
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error getting transactions: " + ex.Message);
            }
        }

        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Post(TransactionDTO transactionDTO)
        {
            if (transactionDTO == null)
                return BadRequest("Invalid Data.");
        
            try
            {
                await _transactionService.Add(transactionDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error making the transaction: "+ ex.Message);
            }
        
            return new CreatedAtRouteResult("GetAllAccountTransactions", 
                new { accountNumber = transactionDTO.AccountNumber }, transactionDTO);
        }
    }
}
