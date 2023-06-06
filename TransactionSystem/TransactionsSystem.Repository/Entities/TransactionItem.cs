using Infrastructure.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionsSystem.Repository.Entities
{
    public class TransactionItem:BaseEntity<Guid>
    {
        public Guid ClientId { get; set; }
        public decimal Summ { get; set; }
        public DateTime DateTime { get; set; }
        public int TypeTransactionId { get; set; }
        public virtual TypeTransaction TypeTransaction { get; set; }
    }
}
