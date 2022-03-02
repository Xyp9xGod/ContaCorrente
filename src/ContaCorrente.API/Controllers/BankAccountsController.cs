﻿using ContaCorrente.Application.DTOs;
using ContaCorrente.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ContaCorrente.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountsController : ControllerBase
    {
        private IBankAccountService _bankAccountService;

        public BankAccountsController(IBankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<BankAccountModelDTO>>> Get()
        {
            try
            {
                var bankAccounts = await _bankAccountService.GetAllAccountsAsync();
                if (bankAccounts == null)
                {
                    return NotFound("Accounts Not Found.");
                }
                return Ok(bankAccounts);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error getting accounts: " + ex.Message);
            }
        }

        [HttpGet("Balance/{accountNumber}", Name = "GetBalance")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<BankAccountModelDTO>>> GetBalance(string accountNumber)
        {
            try
            {
                var bankAccounts = await _bankAccountService.GetByAccountNumberAsync(accountNumber);
                if (bankAccounts == null)
                {
                    return NotFound("Bank Account Not Found.");
                }
                return Ok(bankAccounts);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error getting account: " + ex.Message);
            }
        }

        [HttpGet("History/{accountNumber}", Name = "GetHistory")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<BankAccountDTO>>> GetHistory(string accountNumber)
        {
            try
            {
                var bankAccounts = await _bankAccountService.GetHistoryAsync(accountNumber);
                if (bankAccounts == null)
                {
                    return NotFound("Bank Account Not Found.");
                }
                return Ok(bankAccounts);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error getting account: " + ex.Message);
            }
        }

        [HttpGet("PeriodHistory/{accountNumber}/{startDate}/{finalDate}", Name = "GetPeriodHistory")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<BankAccountDTO>>> GetPeriodHistory(string accountNumber, DateTime startDate, DateTime finalDate)
        {
            try
            {
                var bankAccounts = await _bankAccountService.GetPeriodHistoryAsync(accountNumber, startDate, finalDate);
                if (bankAccounts == null)
                {
                    return NotFound("Bank Account Not Found.");
                }
                return Ok(bankAccounts);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error getting account: " + ex.Message);
            }
        }

        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Post(BankAccountModelDTO bankAccountDTO)
        {
            if (bankAccountDTO == null)
                return BadRequest("Invalid Data.");
        
            try
            {
                await _bankAccountService.Add(bankAccountDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating account: " + ex.Message);
            }
        
            return new CreatedAtRouteResult("GetBalance", 
                new { accountNumber = bankAccountDTO.AccountNumber }, bankAccountDTO);
        }

        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(BankAccountModelDTO bankAccountDTO)
        {
            if (bankAccountDTO == null)
                return BadRequest("Invalid Data.");

            var bankAccount = await _bankAccountService.GetByAccountNumberAsync(bankAccountDTO.AccountNumber);

            if (bankAccount == null)
            {
                return NotFound("Account Not Found.");
            }
            else
            {
                bankAccountDTO.Id = bankAccount.Id;
            }

            try
            {
                await _bankAccountService.Update(bankAccountDTO);
            }
            catch (Exception ex)
            {
                return BadRequest("Error trying to update de account: " + ex.Message);
            }
            return Ok(bankAccountDTO);
        }

        [HttpDelete("{accountNumber}", Name = "DeleteAccount")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<BankAccountDTO>>> Delete(string accountNumber)
        {
            if (string.IsNullOrEmpty(accountNumber))
                return BadRequest("Invalid AccountNumber.");

            try
            {
                var bankAccount = await _bankAccountService.GetByAccountNumberAsync(accountNumber);
                if (bankAccount == null)
                {
                    return NotFound("Bank Account Not Found.");
                }

                await _bankAccountService.Remove(bankAccount.AccountNumber);
                return Ok(bankAccount);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting account: " + ex.Message);
            }
        }
    }
}
