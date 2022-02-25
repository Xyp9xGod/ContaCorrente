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
    public class TransationsController : ControllerBase
    {
        private ITransactionService _transationService;

        public TransationsController(ITransactionService transationService)
        {
            _transationService = transationService;
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
                var transactions = await _transationService.GetAllAccountTransactionsAsync(accountNumber);
                if (transactions.Count() == 0)
                {
                    return NotFound("No movimentations found.");
                }
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
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
                var transactions = await _transationService
                    .GetTransactionsByDateAsync(accountNumber, startDate, finalDate);

                if (transactions.Count() == 0)
                {
                    return NotFound("No movimentations found on this periodo.");
                }
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Post([FromBody] TransactionDTO transactionDTO)
        {
            if (transactionDTO == null)
                return BadRequest("Invalid Data.");
        
            try
            {
                await _transationService.Add(transactionDTO);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        
            return new CreatedAtRouteResult("GetAllAccountTransactions", 
                new { accountNumber = transactionDTO.AccountNumber }, transactionDTO);
        }
    }
}
