using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionsSystem.Repository.Entities;
using TransactionsSystem.Repository.Interfaces;

namespace TransactionsSystem.Repository.Repositories
{
    public class TransactionItemRepository : Repository<TransactionItem, Guid>, ITransactionItemRepository
    {
        public TransactionItemRepository(DbContext context) : base(context)
        {
        }
    }
}
