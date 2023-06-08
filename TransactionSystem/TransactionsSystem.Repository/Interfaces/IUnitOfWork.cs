using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionsSystem.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ITypeTransactionRepository TypeTransactions { get; }
        ITransactionItemRepository Transactions { get; }
        int Complete();
    }
}
