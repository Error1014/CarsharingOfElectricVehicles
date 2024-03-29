﻿using Infrastructure.DTO;
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
        Task<decimal> GetBalance();
        Task<decimal> GetBalance(Guid id);
        Task<TransactionItemDTO> GetTransaction(Guid id);
        Task<Dictionary<Guid, TransactionItemDTO>> GetTransactions(TransactionFilter filter);
        Task<Guid> AddTransaction(TransactionAddDTO transactionItemDTO);
        Task RemoveTransaction(Guid id);
        Task UpdateTransactionItem(Guid id, TransactionAddDTO transactionItemDTO);
    }
}
