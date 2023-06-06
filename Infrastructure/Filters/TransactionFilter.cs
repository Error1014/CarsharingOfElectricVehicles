using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Filters
{
    public class TransactionFilter:PageFilter
    {
        public Guid? ClientId { get; set; }
        public decimal? MinSumm { get; set; }
        public decimal? MaxSumm { get; set; }
        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }
        public List<int>? TypeTransaction { get; set; }

        public TransactionFilter() 
        {
        }
        public TransactionFilter(int numPage, int sizePage) : base(numPage, sizePage)
        {
        }
        public TransactionFilter(int numPage, int sizePage, Guid? clientId, decimal? minSumm, decimal? maxSumm, DateTime? minDate, DateTime? maxDate, List<int>? types):base(numPage, sizePage)
        {
            ClientId = clientId;
            MinSumm = minSumm;
            MaxSumm = maxSumm;
            MinDate = minDate;
            MaxDate = maxDate;
            TypeTransaction = types;
        }

    }
}
