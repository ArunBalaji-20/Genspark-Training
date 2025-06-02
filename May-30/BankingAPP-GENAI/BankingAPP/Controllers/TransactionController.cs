using BankingAPP.Interfaces;
using BankingAPP.Models;
using BankingAPP.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("Deposit")]
        public async Task<ActionResult<Transaction>> DepositAmount([FromBody] TransactionRequestDto transactionRequestDto)
        {
            try
            {
                var result = await _transactionService.Deposit(transactionRequestDto);
                if (result == null)
                {
                    return BadRequest("Check Your inputs");
                }
                return Created("", result);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }

        [HttpPost("Withdraw")]
        public async Task<ActionResult<Transaction>> WithdrawAmount([FromBody] TransactionRequestDto transactionRequestDto)
        {
            try
            {
                var result = await _transactionService.Withdraw(transactionRequestDto);
                if (result == null)
                {
                    return BadRequest("Check Your inputs");
                }
                return Created("", result);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }

        [HttpPost("Transfer")]
        public async Task<ActionResult<Transaction>> TranferAmount([FromBody] TransferRequestDto transferRequestDto)
        {
            try
            {
                var result = await _transactionService.Transfer(transferRequestDto);
                if (result == null)
                {
                    return BadRequest("Check your input");
                }
                return Created("",result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
