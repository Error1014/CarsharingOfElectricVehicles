using Infrastructure.DTO;
using Infrastructure.Filters;
using Infrastructure.HelperModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionsSystem.Service.Interfaces;

namespace TransactionsSystem.Api.Controllers
{
    public class TransactionController : BaseApiController
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransaction(Guid id)
        {
            var result = await _transactionService.GetTransaction(id);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetTransactions([FromQuery] PageFilter pageFilter)
        {
            var result = await _transactionService.GetTransactions(pageFilter);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddTransaction([FromQuery] TransactionItemDTO transactionItemDTO)
        {
            await _transactionService.AddTransaction(transactionItemDTO);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTransaction(Guid id, [FromQuery] TransactionItemDTO transactionItemDTO)
        {
            await _transactionService.UpdateTransactionItem(id, transactionItemDTO);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveTransaction(Guid id)
        {
            await _transactionService.RemoveTransaction(id);
            return Ok();
        }
    }
}
