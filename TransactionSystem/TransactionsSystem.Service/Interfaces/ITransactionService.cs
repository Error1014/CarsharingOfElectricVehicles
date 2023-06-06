using Infrastructure.DTO;
using Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionsSystem.Service.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionItemDTO> GetTransaction(Guid id);
        Task<Dictionary<Guid, TransactionItemDTO>> GetTransactions(PageFilter pageFilter);
        Task AddTransaction(TransactionItemDTO transactionItemDTO);
        Task RemoveTransaction(Guid id);
        Task UpdateTransactionItem(Guid id, TransactionItemDTO transactionItemDTO);
    }
}
