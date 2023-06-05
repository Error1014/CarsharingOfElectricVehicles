using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Filters
{
    public class HistoryRentFilter : PageFilter
    {
        public Guid? ClientId { get; set; }
        public DateTime? DateTimeBeginRent { get; set; }
        public DateTime? DateTimeEndRent { get; set; }
        public decimal? MinKilometersOutsideTariff { get; set; }
        public decimal? MaxKilometersOutsideTariff { get; set; }
        public decimal? MinTotalPrice { get; set; }
        public decimal? MaxTotalPrice { get; set; }
        public HistoryRentFilter() : base()
        {
            DateTimeBeginRent = null;
            DateTimeEndRent = null;
            MinKilometersOutsideTariff = null;
            MaxKilometersOutsideTariff = null;
            MinTotalPrice = null;
            MaxTotalPrice = null;
        }
        public HistoryRentFilter(Guid clientId,int numPage, int sizePage, DateTime? dateTimeBeginRent, DateTime? dateTimeEndRent, decimal? minKilometersOutsideTariff, decimal? maxKilometersOutsideTariff, decimal? minTotalPrice, decimal maxTotalPrice) : base(numPage, sizePage)
        {
            ClientId = clientId;
            DateTimeBeginRent = dateTimeBeginRent;
            DateTimeEndRent = dateTimeEndRent;
            MinKilometersOutsideTariff = minKilometersOutsideTariff;
            MaxKilometersOutsideTariff = maxKilometersOutsideTariff;
            MinTotalPrice = minTotalPrice;
            MaxTotalPrice = maxTotalPrice;
        }
    }
}
