using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionsSystem.Repository.Entities;

namespace TransactionsSystem.Repository.Context
{
    public class TransactionContext : DbContext
    {
        public TransactionContext(DbContextOptions<TransactionContext> options) : base(options)
        {

        }
        public DbSet<TransactionItem> TransactionItem { get; set; }
        public DbSet<TypeTransaction> TypeTransaction { get; set; }
    }
}
