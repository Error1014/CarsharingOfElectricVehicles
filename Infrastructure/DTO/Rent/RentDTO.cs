using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO.Rent
{
    public class RentDTO
    {
        public Guid ClientId { get; set; }
        public Guid CarId { get; set; }
        public DateTime DateTimeBeginBoocking { get; set; }
        public Guid TariffId { get; set; }
        public bool IsFinalSelectCar { get; set; }
        public DateTime DateTimeBeginRent { get; set; }
        public DateTime DateTimeEndRent { get; set; }
        public decimal KilometersOutsideTariff { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
