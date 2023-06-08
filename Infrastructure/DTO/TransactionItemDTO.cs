using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class TransactionItemDTO
    {
        public Guid ClientId { get; set; }
        public decimal Summ { get; set; }
        public DateTime DateTime { get; set; }
        public int TypeTransactionId { get; set; }
    }
}
