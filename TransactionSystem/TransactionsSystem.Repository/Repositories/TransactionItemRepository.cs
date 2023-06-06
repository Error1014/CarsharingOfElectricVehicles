using Infrastructure.Filters;
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

        public async Task<IEnumerable<TransactionItem>> GetTransactions(TransactionFilter transactionFilter)
        {
            var query = Set;
            if (transactionFilter.ClientId.HasValue)
            {
                query = query.Where(x => x.ClientId == transactionFilter.ClientId);
            }
            if (transactionFilter.MinSumm.HasValue)
            {
                query = query.Where(x => x.Summ >= transactionFilter.MinSumm);
            }
            if (transactionFilter.MaxSumm.HasValue)
            {
                query = query.Where(x => x.Summ <= transactionFilter.MaxSumm);
            }
            if (transactionFilter.MinDate.HasValue)
            {
                query = query.Where(x => x.DateTime >= transactionFilter.MinDate);
            }
            if (transactionFilter.MaxDate.HasValue)
            {
                query = query.Where(x => x.DateTime <= transactionFilter.MaxDate);
            }
            var list = new List<TransactionItem>();
            if (transactionFilter.TypeTransaction!=null)
            {
                foreach(var item in query)
                {
                    bool isOk = false;
                    for (int j = 0; j < transactionFilter.TypeTransaction.Count(); j++)
                    {
                        if (item.TypeTransactionId == transactionFilter.TypeTransaction[j])
                        {
                            isOk = true;
                        }
                        else
                        {
                            isOk = false;
                            break;
                        }
                    }
                    if (isOk)
                    {
                        list.Add(item);
                    }
                    
                }
            }
            list = list
                .OrderBy(x => x.Id)
                .Skip((transactionFilter.NumPage - 1) * transactionFilter.SizePage)
                .Take(transactionFilter.SizePage).ToList();
            return list;
        }
    }
}
