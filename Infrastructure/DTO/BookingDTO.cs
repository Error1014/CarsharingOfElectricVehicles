using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class BookingDTO
    {
        public Guid CarId { get; set; }
        public DateTime DateTimeBeginRent { get; set; }
        public Guid TariffId { get; set; }
    }
}
