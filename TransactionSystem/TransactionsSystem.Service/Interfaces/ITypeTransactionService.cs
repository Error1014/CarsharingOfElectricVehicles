using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionsSystem.Service.Interfaces
{
    public interface ITypeTransactionService
    {
        Task<string> GetTypeTransaction(int transactionId);
        Task<Dictionary<int, string>> GetTypeTransactions();
        Task AddTypeTransaction(string typeTransaction);
        Task UpdateTypeTransaction(int id, string value);
        Task RemoveTypeTransiction(int transactionId);
    }
}
