using Infrastructure.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rents.Repository.Entities
{
    public class RentCheque:BaseEntity<Guid>
    {
        public Guid RentId { get; set; }
        public virtual Booking Booking { get; set; }
        public DateTime DateTimeBeginRent { get; set; }
        public DateTime DateTimeEndRent { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
