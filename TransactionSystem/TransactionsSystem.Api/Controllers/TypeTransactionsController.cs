using Infrastructure.Attributes;
using Infrastructure.HelperModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionsSystem.Service.Interfaces;

namespace TransactionsSystem.Api.Controllers
{
    public class TypeTransactionsController : BaseApiController
    {
        private readonly ITypeTransactionService _transactionService;

        public TypeTransactionsController(ITypeTransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetType(int id)
        {
            var result = await _transactionService.GetTypeTransaction(id);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetTypes()
        {
            var result = await _transactionService.GetTypeTransactions();
            return Ok(result);
        }
        [RoleAuthorize("Admin")]
        [HttpPost]
        public async Task<IActionResult> AddType(string value)
        {
            await _transactionService.AddTypeTransaction(value);
            return Ok();
        }
        [RoleAuthorize("Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateType(int id, string value)
        {
            await _transactionService.UpdateTypeTransaction(id, value);
            return Ok();
        }
        [RoleAuthorize("Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveType(int id)
        {
            await _transactionService.RemoveTypeTransiction(id);
            return Ok();
        }
    }
}
