using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionsSystem.Repository.Entities;

namespace TransactionsSystem.Repository.Interfaces
{
    public interface ITransactionItemRepository : IRepository<TransactionItem, Guid>
    {
    }
}
