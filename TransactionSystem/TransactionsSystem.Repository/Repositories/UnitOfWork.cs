using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionsSystem.Repository.Context;
using TransactionsSystem.Repository.Interfaces;

namespace TransactionsSystem.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public UnitOfWork(TransactionContext context)
        {
            _dbContext = context;
            Transactions = new TransactionItemRepository(context);
            TypeTransactions = new TypeTransactionRepository(context);
        }

        public ITransactionItemRepository Transactions { get; private set; }
        public ITypeTransactionRepository TypeTransactions { get; private set; }


        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
