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
        public async Task<ActionResult<IEnumerable<BankAccountDTO>>> Get()
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
                return Problem(ex.Message);
            }
        }

        [HttpGet("{accountNumber}", Name = "GetByAccountNumber")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<BankAccountDTO>>> Get(string accountNumber)
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
                return Problem(ex.Message);
            }
        }

        //[HttpPost]
        //[Produces(MediaTypeNames.Application.Json)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<ActionResult> Post([FromBody] BankAccountDTO bankAccountDTO)
        //{
        //    if (bankAccountDTO == null)
        //        return BadRequest("Invalid Data.");
        //
        //    try
        //    {
        //        await _bankAccountService.Add(bankAccountDTO);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Problem(ex.Message);
        //    }
        //
        //    return new CreatedAtRouteResult("GetByAccountNumber", 
        //        new { accountNumber = bankAccountDTO.AccountNumber }, bankAccountDTO);
        //}

        //TODO - METODO PUT

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
                return Problem(ex.Message);
            }
        }
    }
}